namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;

public record LgsmSetupConfiguration
{
    public Dictionary<string, string> AvailableGameServers { get; set; } = new();
}
