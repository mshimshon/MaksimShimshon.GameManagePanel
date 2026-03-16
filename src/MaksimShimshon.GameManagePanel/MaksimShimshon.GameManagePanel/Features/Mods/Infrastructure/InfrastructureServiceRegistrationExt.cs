using MaksimShimshon.GameManagePanel.Features.Mods.Application;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto.Mapping;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure;

public static class InfrastructureServiceRegistrationExt
{
    public static void RegisterModInfrastructureServices(this IServiceCollection services)
    {
        services.RegisterModApplicationServices();
        services.AddScoped<IModListService, ModListService>();
        services.AddCoreMapHandler<ModEntityToModResponse>();
        services.AddCoreMapHandler<ModListEntityToModListResponse>();
        services.AddCoreMapHandler<ModListResponseToModListEntity>();
        services.AddCoreMapHandler<ModResponseToModEntity>();
    }
}
