using System;

namespace Licensing.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid ApplicationId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string TargetPersona { get; set; } = string.Empty; // "Operator" or "Officer"
    public bool IsRead { get; set; }
}
