using Licensing.Application.DTOs;
using Licensing.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Licensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ReviewController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("applications")]
    public async Task<IActionResult> GetAll()
    {
        var apps = await _applicationService.GetAllApplicationsAsync();
        return Ok(apps);
    }

    [HttpPost("{id}/feedback")]
    public async Task<IActionResult> ProvideFeedback(Guid id, [FromBody] ProvideFeedbackRequest request)
    {
        await _applicationService.ProvideFeedbackAsync(id, request);
        return Ok();
    }

    [HttpPost("{id}/decision")]
    public async Task<IActionResult> SubmitDecision(Guid id, [FromBody] ReviewApplicationRequest request)
    {
        await _applicationService.SubmitReviewAsync(id, request);
        return Ok();
    }

    [HttpGet("{id}/snapshots")]
    public async Task<IActionResult> GetSnapshots(Guid id)
    {
        var snapshots = await _applicationService.GetSnapshotsAsync(id);
        return Ok(snapshots);
    }
}
