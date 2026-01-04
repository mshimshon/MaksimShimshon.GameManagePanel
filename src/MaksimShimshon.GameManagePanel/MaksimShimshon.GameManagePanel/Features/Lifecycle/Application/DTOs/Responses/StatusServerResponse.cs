namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusServerResponse
{
    public string Name { get; set; } = default!;
    public string Ip { get; set; } = default!;
    public int Port { get; set; }
    public int MaxPlayers { get; set; }
}
