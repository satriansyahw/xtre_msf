using Licensing.Application.DTOs;
using Licensing.Application.Services;
using Licensing.Domain.Enums;
using Licensing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Licensing.Tests.Unit;

public class ApplicationServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ApplicationService _service;

    public ApplicationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _service = new ApplicationService(_dbContext);
    }

    [Fact]
    public async Task SubmitApplicationAsync_ShouldCreateApplicationWithReferenceNumber()
    {
        // Arrange
        var request = new SubmitApplicationRequest
        {
            ApplicantName = "Test Applicant",
            BusinessName = "Test Business",
            ContactEmail = "test@example.com",
            DataJson = "{}",
            DocumentIds = new List<Guid>()
        };

        // Act
        var appId = await _service.SubmitApplicationAsync(request);

        // Assert
        var app = await _dbContext.Applications.FindAsync(appId);
        Assert.NotNull(app);
        Assert.StartsWith("LIC-", app.ReferenceNumber);
        Assert.Equal(ApplicationStatus.ApplicationReceived, app.Status);
        
        var snapshot = await _dbContext.ApplicationSnapshots.FirstOrDefaultAsync(s => s.ApplicationId == appId);
        Assert.NotNull(snapshot);
        Assert.Equal(1, snapshot.Version);
    }

    [Fact]
    public async Task SubmitReviewAsync_WithAllowedStatus_ShouldUpdateStatus()
    {
        // Arrange
        var appId = await CreateTestApplication();
        var request = new ReviewApplicationRequest
        {
            NewStatus = ApplicationStatus.Approved,
            GlobalComment = "Looks good"
        };

        // Act
        await _service.SubmitReviewAsync(appId, request);

        // Assert
        var app = await _dbContext.Applications.FindAsync(appId);
        Assert.Equal(ApplicationStatus.Approved, app.Status);
        
        var feedback = await _dbContext.Feedbacks.FirstOrDefaultAsync(f => f.ApplicationId == appId && f.FieldName == "Overall Decision");
        Assert.NotNull(feedback);
        Assert.Equal("Looks good", feedback.Comment);
    }

    [Fact]
    public async Task SubmitReviewAsync_WithInvalidStatus_ShouldThrowException()
    {
        // Arrange
        var appId = await CreateTestApplication();
        var request = new ReviewApplicationRequest
        {
            NewStatus = ApplicationStatus.ApplicationReceived // Officers shouldn't reset to received
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.SubmitReviewAsync(appId, request));
    }

    [Fact]
    public async Task ResubmitApplicationAsync_ShouldIncrementVersion()
    {
        // Arrange
        var appId = await CreateTestApplication();
        
        // Change status to pending resubmission so resubmit works
        var app = await _dbContext.Applications.FindAsync(appId);
        app.Status = ApplicationStatus.PendingPreSiteResubmission;
        await _dbContext.SaveChangesAsync();

        var request = new SubmitApplicationRequest
        {
            ApplicantName = "Updated Name",
            BusinessName = "Updated Business",
            ContactEmail = "updated@example.com",
            DataJson = "{\"updated\": true}",
            DocumentIds = new List<Guid>()
        };

        // Act
        await _service.ResubmitApplicationAsync(appId, request);

        // Assert
        var updatedApp = await _dbContext.Applications.FindAsync(appId);
        Assert.Equal("Updated Name", updatedApp.ApplicantName);
        Assert.Equal(ApplicationStatus.PreSiteResubmitted, updatedApp.Status);

        var snapshots = await _dbContext.ApplicationSnapshots
            .Where(s => s.ApplicationId == appId)
            .OrderByDescending(s => s.Version)
            .ToListAsync();
        
        Assert.Equal(2, snapshots.Count);
        Assert.Equal(2, snapshots[0].Version);
        Assert.Contains("updated", snapshots[0].DataJson);
    }

    private async Task<Guid> CreateTestApplication()
    {
        var request = new SubmitApplicationRequest
        {
            ApplicantName = "Base App",
            BusinessName = "Base Biz",
            ContactEmail = "base@example.com",
            DataJson = "{}",
            DocumentIds = new List<Guid>()
        };
        return await _service.SubmitApplicationAsync(request);
    }
}
