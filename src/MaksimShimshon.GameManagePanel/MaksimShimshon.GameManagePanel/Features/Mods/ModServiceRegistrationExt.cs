using MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Hooks.Components.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Mods;

public static class ModServiceRegistrationExt
{
    public static void AddModFeatureServices(this IServiceCollection services, bool isMaster)
    {
        services.AddScoped<IModListSelectorViewModel, ModListSelectorViewModel>();
        services.AddScoped<IWidgetModlistSelectorViewModel, WidgetModlistSelectorViewModel>();

        services.RegisterModInfrastructureServices();

    }
    public static async Task RuntimeModInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {

    }
}
