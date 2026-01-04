namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record GameStartupParameterRelatedToResponse
{
    public string Key { get; init; } = default!;
    public string Constraint { get; init; } = default!;
    public string Message { get; init; } = default!;
}
