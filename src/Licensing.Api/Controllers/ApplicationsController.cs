using Licensing.Application.DTOs;
using Licensing.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitApplicationRequest request)
    {
        if (request == null) return BadRequest();
        
        var id = await _applicationService.SubmitApplicationAsync(request);
        return Ok(new { Id = id });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        var apps = await _applicationService.GetMyApplicationsAsync();
        return Ok(apps);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(Guid id)
    {
        var app = await _applicationService.GetDetailsAsync(id);
        if (app == null) return NotFound();

        return Ok(app);
    }

    [HttpPost("{id}/resubmit")]
    public async Task<IActionResult> Resubmit(Guid id, [FromBody] SubmitApplicationRequest request)
    {
        try 
        {
            await _applicationService.ResubmitApplicationAsync(id, request);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("notifications")]
    public async Task<IActionResult> GetNotifications([FromQuery] string persona)
    {
        if (string.IsNullOrEmpty(persona)) return BadRequest();
        var notifications = await _applicationService.GetNotificationsAsync(persona);
        return Ok(notifications);
    }

    [HttpPost("notifications/{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await _applicationService.MarkNotificationAsReadAsync(id);
        return Ok();
    }

    [HttpGet("{id}/audit")]
    public async Task<ActionResult<List<AuditLogResponse>>> GetAuditTrail(Guid id)
    {
        return Ok(await _applicationService.GetAuditTrailAsync(id));
    }
}
