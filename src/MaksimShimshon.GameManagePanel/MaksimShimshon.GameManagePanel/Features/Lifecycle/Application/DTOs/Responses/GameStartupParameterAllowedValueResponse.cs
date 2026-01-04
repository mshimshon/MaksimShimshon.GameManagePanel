namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record GameStartupParameterAllowedValueResponse
{
    public string Value { get; init; } = default!;
    public string Label { get; init; } = default!;
}
