namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record GameStartupParameterRelatedToResponse
{
    public string Key { get; init; } = default!;
    public string Constraint { get; init; } = default!;
    public string Message { get; init; } = default!;
}
