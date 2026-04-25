using System;

namespace Licensing.Application.DTOs;

public class ApplicationSnapshotResponse
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string DataJson { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
