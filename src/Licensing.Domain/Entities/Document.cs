namespace Licensing.Domain.Entities;

public class Document : BaseEntity
{
    public Guid ApplicationId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    
    // AI Verification Status: Pending, Verified, Flagged
    public string AIStatus { get; set; } = "Pending"; 
    
    public LicenseApplication Application { get; set; } = null!;
}
