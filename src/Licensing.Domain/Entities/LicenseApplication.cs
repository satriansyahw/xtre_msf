using System.Collections.Generic;
using Licensing.Domain.Enums;

namespace Licensing.Domain.Entities;

public class LicenseApplication : BaseEntity
{
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.ApplicationReceived;
    
    // JSON data representation of the application form
    public string DataJson { get; set; } = "{}";

    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public ICollection<ApplicationSnapshot> Snapshots { get; set; } = new List<ApplicationSnapshot>();
}
