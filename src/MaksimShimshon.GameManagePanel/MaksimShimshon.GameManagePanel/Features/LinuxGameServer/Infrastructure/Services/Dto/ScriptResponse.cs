using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services.Dto;

internal record ScriptResponse
{
    public bool Completed { get; set; }
    [JsonPropertyName("failure_message")]
    public string? Failure { get; set; }
}
