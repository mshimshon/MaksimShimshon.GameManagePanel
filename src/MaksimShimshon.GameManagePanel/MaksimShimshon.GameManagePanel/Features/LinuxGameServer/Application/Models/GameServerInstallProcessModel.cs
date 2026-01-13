namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;

public record GameServerInstallProcessModel
{
    public bool Failed { get; set; }
    public bool IsInstalling { get; init; }
    public string CurrentStep { get; init; } = default!;
    public string Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;



}
