using Licensing.Application.DTOs;
using Licensing.Application.Interfaces;
using Licensing.Domain.Constants;
using Licensing.Domain.Entities;
using Licensing.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licensing.Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationDbContext _dbContext;

    // Fixed: Allowed status transitions for Officers
    private static readonly HashSet<ApplicationStatus> AllowedOfficerDecisions = new()
    {
        ApplicationStatus.Approved,
        ApplicationStatus.Rejected,
        ApplicationStatus.PendingPreSiteResubmission,
        ApplicationStatus.PendingPostSiteResubmission,
        ApplicationStatus.UnderReview
    };

    public ApplicationService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> SubmitApplicationAsync(SubmitApplicationRequest request)
    {
        var application = new LicenseApplication
        {
            ApplicantName = request.ApplicantName,
            BusinessName = request.BusinessName,
            ContactEmail = request.ContactEmail,
            DataJson = request.DataJson,
            ReferenceNumber = $"LIC-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}",
            Status = ApplicationStatus.ApplicationReceived
        };

        if (request.DocumentIds.Any())
        {
            var documents = await _dbContext.Documents
                .Where(d => request.DocumentIds.Contains(d.Id))
                .ToListAsync();

            foreach (var doc in documents)
            {
                doc.ApplicationId = application.Id;
            }
        }

        _dbContext.Applications.Add(application);

        var snapshot = new ApplicationSnapshot
        {
            ApplicationId = application.Id,
            Version = 1,
            DataJson = request.DataJson,
            SubmittedBy = "Operator"
        };
        _dbContext.ApplicationSnapshots.Add(snapshot);
        
        // Log initial submission
        _dbContext.AuditLogs.Add(new AuditLog
        {
            ApplicationId = application.Id,
            NewStatus = application.Status,
            ChangedBy = "Operator",
            Comment = "Initial application submission"
        });

        await _dbContext.SaveChangesAsync();
        return application.Id;
    }

    public async Task<List<ApplicationResponse>> GetMyApplicationsAsync()
    {
        // TODO: Filter by current user when Auth is implemented
        return await GetApplicationResponsesAsync(_dbContext.Applications);
    }

    public async Task<List<ApplicationResponse>> GetAllApplicationsAsync()
    {
        return await GetApplicationResponsesAsync(_dbContext.Applications);
    }

    private async Task<List<ApplicationResponse>> GetApplicationResponsesAsync(IQueryable<LicenseApplication> query)
    {
        return await query
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new ApplicationResponse
            {
                Id = a.Id,
                ReferenceNumber = a.ReferenceNumber,
                ApplicantName = a.ApplicantName,
                BusinessName = a.BusinessName,
                ContactEmail = a.ContactEmail,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ApplicationResponse?> GetDetailsAsync(Guid id)
    {
        var app = await _dbContext.Applications
            .Include(a => a.Documents)
            .Include(a => a.Feedbacks)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (app == null) return null;

        return new ApplicationResponse
        {
            Id = app.Id,
            ReferenceNumber = app.ReferenceNumber,
            ApplicantName = app.ApplicantName,
            BusinessName = app.BusinessName,
            ContactEmail = app.ContactEmail, // Fixed: Added ContactEmail
            Status = app.Status.ToString(),
            DataJson = app.DataJson, // Fixed: Added DataJson
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
    }

    public async Task ProvideFeedbackAsync(Guid applicationId, ProvideFeedbackRequest request)
    {
        var feedback = new Feedback
        {
            ApplicationId = applicationId,
            FieldName = request.FieldName,
            Comment = request.Comment,
            OfficerName = "System Officer" // Mocked
        };

        _dbContext.Feedbacks.Add(feedback);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SubmitReviewAsync(Guid applicationId, ReviewApplicationRequest request)
    {
        // Fixed: Validate status transitions
        if (!AllowedOfficerDecisions.Contains(request.NewStatus))
        {
            throw new InvalidOperationException($"Status '{request.NewStatus}' is not a valid officer decision.");
        }

        var app = await _dbContext.Applications.FindAsync(applicationId);
        if (app == null)
        {
            throw new KeyNotFoundException($"Application {applicationId} not found.");
        }

        var oldStatus = app.Status;
        app.Status = request.NewStatus;
        app.UpdatedAt = DateTime.UtcNow;

        // Log transition
        _dbContext.AuditLogs.Add(new AuditLog
        {
            ApplicationId = applicationId,
            OldStatus = oldStatus,
            NewStatus = request.NewStatus,
            ChangedBy = "Officer",
            Comment = request.GlobalComment
        });

        if (!string.IsNullOrWhiteSpace(request.GlobalComment))
        {
            _dbContext.Feedbacks.Add(new Feedback
            {
                ApplicationId = applicationId,
                FieldName = "Overall Decision",
                Comment = request.GlobalComment,
                OfficerName = "System Officer"
            });
        }

        await _dbContext.SaveChangesAsync();

        // Create notification for Operator
        var notification = new Notification
        {
            ApplicationId = applicationId,
            Message = $"Your application {app.ReferenceNumber} status has changed to {request.NewStatus}.",
            TargetPersona = "Operator"
        };
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ApplicationSnapshotResponse>> GetSnapshotsAsync(Guid applicationId)
    {
        return await _dbContext.ApplicationSnapshots
            .Where(s => s.ApplicationId == applicationId)
            .OrderByDescending(s => s.Version)
            .Select(s => new ApplicationSnapshotResponse
            {
                Id = s.Id,
                Version = s.Version,
                DataJson = s.DataJson,
                SubmittedBy = s.SubmittedBy,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();
    }

    public async Task ResubmitApplicationAsync(Guid applicationId, SubmitApplicationRequest request)
    {
        var app = await _dbContext.Applications
            .Include(a => a.Documents)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        if (app == null) throw new KeyNotFoundException($"Application {applicationId} not found.");

        // Update application data
        app.ApplicantName = request.ApplicantName;
        app.BusinessName = request.BusinessName;
        app.ContactEmail = request.ContactEmail;
        app.DataJson = request.DataJson;
        app.UpdatedAt = DateTime.UtcNow;

        // Determine next status
        var oldStatus = app.Status;
        var newStatus = app.Status == ApplicationStatus.PendingPreSiteResubmission 
            ? ApplicationStatus.PreSiteResubmitted 
            : ApplicationStatus.PostSiteClarificationResubmitted;
            
        app.Status = newStatus;

        // Log transition
        _dbContext.AuditLogs.Add(new AuditLog
        {
            ApplicationId = applicationId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedBy = "Operator",
            Comment = "Application resubmitted with updates"
        });

        // Sync documents
        if (request.DocumentIds.Any())
        {
            var currentDocIds = app.Documents.Select(d => d.Id).ToList();
            var toAdd = request.DocumentIds.Except(currentDocIds).ToList();

            if (toAdd.Any())
            {
                var newDocs = await _dbContext.Documents
                    .Where(d => toAdd.Contains(d.Id))
                    .ToListAsync();
                
                foreach (var doc in newDocs)
                {
                    doc.ApplicationId = applicationId;
                }
            }
        }

        // Get latest version for snapshot
        var latestVersion = await _dbContext.ApplicationSnapshots
            .Where(s => s.ApplicationId == applicationId)
            .MaxAsync(s => s.Version);

        var snapshot = new ApplicationSnapshot
        {
            ApplicationId = applicationId,
            Version = latestVersion + 1,
            DataJson = request.DataJson,
            SubmittedBy = "Operator (Resubmission)"
        };
        _dbContext.ApplicationSnapshots.Add(snapshot);

        // Resolve all existing feedbacks
        var unresolvedFeedbacks = await _dbContext.Feedbacks
            .Where(f => f.ApplicationId == applicationId && !f.IsResolved)
            .ToListAsync();
        
        foreach (var f in unresolvedFeedbacks)
        {
            f.IsResolved = true;
            f.UpdatedAt = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();

        // Create notification for Officer
        var notification = new Notification
        {
            ApplicationId = applicationId,
            Message = $"Application {app.ReferenceNumber} has been resubmitted by the Operator.",
            TargetPersona = "Officer"
        };
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<NotificationResponse>> GetNotificationsAsync(string persona)
    {
        return await _dbContext.Notifications
            .Where(n => n.TargetPersona == persona)
            .OrderByDescending(n => n.CreatedAt)
            .Take(10)
            .Select(n => new NotificationResponse
            {
                Id = n.Id,
                ApplicationId = n.ApplicationId,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();
    }

    public async Task MarkNotificationAsReadAsync(Guid notificationId)
    {
        var notification = await _dbContext.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<AuditLogResponse>> GetAuditTrailAsync(Guid applicationId)
    {
        return await _dbContext.AuditLogs
            .Where(l => l.ApplicationId == applicationId)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new AuditLogResponse
            {
                OldStatus = l.OldStatus != null ? l.OldStatus.ToString() : null,
                NewStatus = l.NewStatus.ToString(),
                ChangedBy = l.ChangedBy,
                Comment = l.Comment,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync();
    }
}
