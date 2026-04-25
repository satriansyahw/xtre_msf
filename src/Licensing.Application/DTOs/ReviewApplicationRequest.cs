using Licensing.Domain.Enums;

namespace Licensing.Application.DTOs;

public class ReviewApplicationRequest
{
    public ApplicationStatus NewStatus { get; set; }
    public string GlobalComment { get; set; } = string.Empty;
}
