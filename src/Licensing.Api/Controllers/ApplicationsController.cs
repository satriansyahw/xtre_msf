using Licensing.Application.DTOs;
using Licensing.Application.Interfaces;
using Licensing.Domain.Entities;
using Licensing.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationDbContext _dbContext;

    public ApplicationsController(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(SubmitApplicationRequest request)
    {
        var application = new LicenseApplication
        {
            ApplicantName = request.ApplicantName,
            BusinessName = request.BusinessName,
            ContactEmail = request.ContactEmail,
            DataJson = request.DataJson,
            ReferenceNumber = $"LIC-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}",
            Status = ApplicationStatus.ApplicationReceived
        };

        // Link existing documents
        foreach (var docId in request.DocumentIds)
        {
            var doc = await _dbContext.Documents.FindAsync(docId);
            if (doc != null)
            {
                doc.ApplicationId = application.Id;
            }
        }

        _dbContext.Applications.Add(application);

        // Create Initial Snapshot
        var snapshot = new ApplicationSnapshot
        {
            ApplicationId = application.Id,
            Version = 1,
            DataJson = request.DataJson,
            SubmittedBy = "Operator"
        };
        _dbContext.ApplicationSnapshots.Add(snapshot);

        await _dbContext.SaveChangesAsync();

        return Ok(new { application.Id, application.ReferenceNumber });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        // In a real app, we would filter by current user email/id
        var apps = await _dbContext.Applications
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new ApplicationResponse
            {
                Id = a.Id,
                ReferenceNumber = a.ReferenceNumber,
                ApplicantName = a.ApplicantName,
                BusinessName = a.BusinessName,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(apps);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(Guid id)
    {
        var app = await _dbContext.Applications
            .Include(a => a.Documents)
            .Include(a => a.Feedbacks)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (app == null) return NotFound();

        var response = new ApplicationResponse
        {
            Id = app.Id,
            ReferenceNumber = app.ReferenceNumber,
            ApplicantName = app.ApplicantName,
            BusinessName = app.BusinessName,
            Status = app.Status.ToString(),
            CreatedAt = app.CreatedAt,
            Documents = app.Documents.Select(d => new DocumentResponse
            {
                Id = d.Id,
                FileName = d.FileName,
                AIStatus = d.AIStatus
            }).ToList(),
            Feedbacks = app.Feedbacks.Select(f => new FeedbackResponse
            {
                FieldName = f.FieldName,
                Comment = f.Comment,
                IsResolved = f.IsResolved
            }).ToList()
        };

        return Ok(response);
    }
}
