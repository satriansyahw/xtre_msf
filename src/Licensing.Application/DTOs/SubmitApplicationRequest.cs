using System;
using System.Collections.Generic;

namespace Licensing.Application.DTOs;

public class SubmitApplicationRequest
{
    public string ApplicantName { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    
    // JSON data representation of the application form
    public string DataJson { get; set; } = "{}";
    
    public List<Guid> DocumentIds { get; set; } = new();
}
