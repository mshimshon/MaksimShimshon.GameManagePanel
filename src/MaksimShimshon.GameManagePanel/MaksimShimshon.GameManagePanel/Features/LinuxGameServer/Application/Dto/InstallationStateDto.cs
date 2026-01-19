namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto;

public record InstallationStateDto
{
    public string Id { get; init; } = default!;
    public DateTime InstallDate { get; init; }
}
