using Microsoft.AspNetCore.Http;

namespace Licensing.Api.Models;

public class UploadRequest
{
    public IFormFile? File { get; set; }
}
