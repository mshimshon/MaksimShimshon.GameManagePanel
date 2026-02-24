using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Kernel.Dto;

public record ScriptResponse
{
    public bool Completed { get; set; }
    [JsonPropertyName("failure_message")]
    public string? Failure { get; set; }
}
