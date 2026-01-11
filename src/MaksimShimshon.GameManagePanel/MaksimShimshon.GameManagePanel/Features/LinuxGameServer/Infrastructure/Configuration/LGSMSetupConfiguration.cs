namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;

public record LGSMSetupConfiguration
{
    public Dictionary<string, string> AvailableGameServers { get; set; } = new();
}
