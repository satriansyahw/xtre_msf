namespace Licensing.Domain.Entities;

public class ApplicationSnapshot : BaseEntity
{
    public Guid ApplicationId { get; set; }
    public int Version { get; set; }
    public string DataJson { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    
    public LicenseApplication Application { get; set; } = null!;
}
