namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Dto;

public record InstallationStateDto
{
    public string Id { get; init; } = default!;
    public DateTime InstallDate { get; init; }
}
