namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record GameStartupParameterKeyValueResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
