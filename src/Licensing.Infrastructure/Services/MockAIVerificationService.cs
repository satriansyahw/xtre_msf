using Licensing.Application.Interfaces;
using Licensing.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Licensing.Infrastructure.Services;

public class MockAIVerificationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MockAIVerificationService> _logger;

    public MockAIVerificationService(IServiceProvider serviceProvider, ILogger<MockAIVerificationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var pendingDocuments = await dbContext.Documents
                    .Where(d => d.AIStatus == "Pending")
                    .ToListAsync(stoppingToken);

                foreach (var doc in pendingDocuments)
                {
                    // Simulate processing time
                    await Task.Delay(5000, stoppingToken);

                    // Randomly verify or flag (80% verified, 20% flagged)
                    doc.AIStatus = Random.Shared.Next(0, 100) < 80 ? "Verified" : "Flagged";
                    doc.UpdatedAt = DateTime.UtcNow;
                    
                    _logger.LogInformation("Document {DocId} ({FileName}) AI status updated to {Status}", 
                        doc.Id, doc.FileName, doc.AIStatus);
                }

                if (pendingDocuments.Any())
                {
                    await dbContext.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing mock AI verification.");
            }

            await Task.Delay(10000, stoppingToken); // Check every 10 seconds
        }
    }
}
