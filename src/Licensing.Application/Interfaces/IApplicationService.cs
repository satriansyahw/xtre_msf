using Licensing.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licensing.Application.Interfaces;

public interface IApplicationService
{
    Task<Guid> SubmitApplicationAsync(SubmitApplicationRequest request);
    Task<List<ApplicationResponse>> GetMyApplicationsAsync();
    Task<ApplicationResponse?> GetDetailsAsync(Guid id);
}
