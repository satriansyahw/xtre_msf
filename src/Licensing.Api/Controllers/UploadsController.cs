using Licensing.Application.Interfaces;
using Licensing.Domain.Entities;
using Licensing.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Licensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadsController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IApplicationDbContext _dbContext;

    public UploadsController(IFileStorageService fileStorageService, IApplicationDbContext dbContext)
    {
        _fileStorageService = fileStorageService;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Fixed: Streaming file directly to storage service instead of loading into byte[]
        using var stream = file.OpenReadStream();
        var filePath = await _fileStorageService.SaveFileAsync(stream, file.FileName, file.ContentType);

        var document = new Document
        {
            FileName = file.FileName,
            FilePath = filePath,
            ContentType = file.ContentType,
            AIStatus = AIVerificationStatus.Pending // Using constant
        };

        _dbContext.Documents.Add(document);
        await _dbContext.SaveChangesAsync();

        return Ok(new { document.Id, document.FileName, document.AIStatus });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStatus(Guid id)
    {
        var document = await _dbContext.Documents.FindAsync(id);
        if (document == null) return NotFound();

        return Ok(new DocumentResponse
        {
            Id = document.Id,
            FileName = document.FileName,
            AIStatus = document.AIStatus
        });
    }
}
