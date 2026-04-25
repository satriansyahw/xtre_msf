namespace Licensing.Domain.Entities;

public class Feedback : BaseEntity
{
    public Guid ApplicationId { get; set; }
    
    // Links to a specific section or field name in the JSON data
    public string FieldName { get; set; } = string.Empty; 
    public string Comment { get; set; } = string.Empty;
    public string OfficerName { get; set; } = "System Officer";
    
    // To track if the feedback has been addressed by the operator
    public bool IsResolved { get; set; } = false;

    public LicenseApplication Application { get; set; } = null!;
}
