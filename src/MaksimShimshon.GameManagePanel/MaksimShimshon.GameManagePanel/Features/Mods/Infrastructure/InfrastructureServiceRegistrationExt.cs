using MaksimShimshon.GameManagePanel.Features.Mods.Application;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure;

public static class InfrastructureServiceRegistrationExt
{
    public static void RegisterModInfrastructureServices(this IServiceCollection services)
    {
        services.RegisterModApplicationServices();
    }
}
