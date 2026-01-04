namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record GameStartupParameterValidationResponse
{
    public int? Min { get; init; }
    public int? Max { get; init; }
    public int? MinLength { get; init; }
    public int? MaxLength { get; init; }
    public string? Pattern { get; init; }
    public List<GameStartupParameterAllowedValueResponse>? AllowedValues { get; init; }
    public string? UnitPrefix { get; init; }
    public string? UnitSuffix { get; init; }
}
