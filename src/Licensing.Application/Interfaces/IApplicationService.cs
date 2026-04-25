using Licensing.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licensing.Application.Interfaces;

public interface IApplicationService
{
    Task<Guid> SubmitApplicationAsync(SubmitApplicationRequest request);
    Task<List<ApplicationResponse>> GetMyApplicationsAsync();
    Task<List<ApplicationResponse>> GetAllApplicationsAsync(); // For Officers
    Task<ApplicationResponse?> GetDetailsAsync(Guid id);
    Task ProvideFeedbackAsync(Guid applicationId, ProvideFeedbackRequest request);
    Task SubmitReviewAsync(Guid applicationId, ReviewApplicationRequest request);
    Task<List<ApplicationSnapshotResponse>> GetSnapshotsAsync(Guid applicationId);
    Task ResubmitApplicationAsync(Guid applicationId, SubmitApplicationRequest request);
}
