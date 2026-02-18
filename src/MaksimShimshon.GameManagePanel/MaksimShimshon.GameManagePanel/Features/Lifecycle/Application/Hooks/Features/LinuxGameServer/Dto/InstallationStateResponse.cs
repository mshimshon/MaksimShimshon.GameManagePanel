namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Hooks.Features.LinuxGameServer.Dto;

internal sealed record InstallationStateResponse
{
    public bool IsInstallationCompleted { get; init; }
    public bool IsInstalledGameDiskLoaded { get; init; }
    public bool IsProgressDiskLoaded { get; init; }
}
