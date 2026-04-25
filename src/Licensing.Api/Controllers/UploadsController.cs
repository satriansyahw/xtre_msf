using Licensing.Application.Interfaces;
using Licensing.Domain.Entities;
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

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var content = memoryStream.ToArray();

        var filePath = await _fileStorageService.SaveFileAsync(content, file.FileName, file.ContentType);

        var document = new Document
        {
            FileName = file.FileName,
            FilePath = filePath,
            ContentType = file.ContentType,
            AIStatus = "Pending"
        };

        _dbContext.Documents.Add(document);
        await _dbContext.SaveChangesAsync();

        return Ok(new { document.Id, document.FileName, document.AIStatus });
    }
}
