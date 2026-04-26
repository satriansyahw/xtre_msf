using Licensing.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Application.DTOs;

public class ReviewApplicationRequest
{
    [Required]
    public ApplicationStatus NewStatus { get; set; }

    [Required(ErrorMessage = "Decision comment is required")]
    public string GlobalComment { get; set; } = string.Empty;
}
