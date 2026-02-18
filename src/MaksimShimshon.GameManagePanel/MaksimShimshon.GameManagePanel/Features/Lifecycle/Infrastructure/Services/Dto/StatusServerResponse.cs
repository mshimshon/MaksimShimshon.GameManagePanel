using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record StatusServerResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("ip")]
    public string Ip { get; set; } = default!;

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("max_players")]
    public int MaxPlayers { get; set; }
}
