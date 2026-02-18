namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record GameStartupParameterAllowedValueResponse
{
    public string Value { get; init; } = default!;
    public string Label { get; init; } = default!;
}
