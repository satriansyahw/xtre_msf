using System;

namespace Licensing.Application.DTOs;

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string AIStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
