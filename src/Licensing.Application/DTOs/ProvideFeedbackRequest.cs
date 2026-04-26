using System.ComponentModel.DataAnnotations;

namespace Licensing.Application.DTOs;

public class ProvideFeedbackRequest
{
    [Required(ErrorMessage = "Field Name is required")]
    public string FieldName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Feedback comment is required")]
    [StringLength(500, MinimumLength = 5)]
    public string Comment { get; set; } = string.Empty;
}
