namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto;

public sealed record InstallationProgressStateDto
{
    public string? FailureReason { get; set; }
    public bool IsInstalling { get; init; }
    public string CurrentStep { get; init; } = default!;
    public string Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
}
