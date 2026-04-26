using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Application.DTOs;

public class SubmitApplicationRequest
{
    [Required(ErrorMessage = "Applicant Name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string ApplicantName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Business Name is required")]
    public string BusinessName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string ContactEmail { get; set; } = string.Empty;
    
    public string DataJson { get; set; } = "{}";
    
    public List<Guid> DocumentIds { get; set; } = new();
}
