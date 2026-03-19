using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto;

internal class GameInfoResponse
{
    [JsonPropertyName("mod_schema")]
    public Dictionary<string, ModSchematicResponse>? Schema { get; set; }
}
