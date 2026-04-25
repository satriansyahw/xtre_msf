using System;
using Licensing.Domain.Enums;

namespace Licensing.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid ApplicationId { get; set; }
    public LicenseApplication Application { get; set; } = null!;

    public ApplicationStatus? OldStatus { get; set; }
    public ApplicationStatus NewStatus { get; set; }
    
    public string ChangedBy { get; set; } = string.Empty; // e.g. "Officer", "Operator", "System"
    public string? Comment { get; set; }
}
