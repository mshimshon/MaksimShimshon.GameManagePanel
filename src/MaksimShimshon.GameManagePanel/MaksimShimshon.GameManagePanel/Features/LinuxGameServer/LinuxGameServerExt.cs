using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer;

public static class LinuxGameServerExt
{
    public static void AddLinuxGameServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var config =
            configuration.GetSection("LinuxGameServer")?.Get<LinuxGameServerConfiguration>() ??
            new LinuxGameServerConfiguration();

        services.AddScoped(sp => config);
    }
}
