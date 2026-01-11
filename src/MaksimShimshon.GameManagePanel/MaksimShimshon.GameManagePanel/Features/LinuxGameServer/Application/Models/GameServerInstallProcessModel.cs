namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;

public record GameServerInstallProcessModel
{
    public bool Failed => ErrorMessage != default;
    public bool IsInstalling { get; init; }
    public string CurrentStep { get; init; } = default!;
    public string? ErrorMessage { get; init; }



}
