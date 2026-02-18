namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record GameStartupParameterKeyValueResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
