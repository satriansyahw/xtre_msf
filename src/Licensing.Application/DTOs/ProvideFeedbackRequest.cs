namespace Licensing.Application.DTOs;

public class ProvideFeedbackRequest
{
    public string FieldName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}
