using System;
using System.Collections.Generic;

namespace Licensing.Application.DTOs;

public class ApplicationResponse
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Human-readable status for the current persona
    public string DataJson { get; set; } = "{}";
    public DateTime CreatedAt { get; set; }
    
    public List<DocumentResponse> Documents { get; set; } = new();
    public List<FeedbackResponse> Feedbacks { get; set; } = new();
}

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string AIStatus { get; set; } = string.Empty;
}

public class FeedbackResponse
{
    public string FieldName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
}
