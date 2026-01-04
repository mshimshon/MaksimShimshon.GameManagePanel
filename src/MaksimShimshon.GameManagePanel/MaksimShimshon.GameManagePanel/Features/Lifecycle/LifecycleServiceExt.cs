using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle;

internal static class LifecycleServiceExt
{
    public static void AddLifecycleFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<LifecycleViewModel>();
        services.AddScoped<LifecycleStartupParameterViewModel>();
        services.AddTransient<LifecycleStartupParameterFieldViewModel>();
        services.AddScoped<LifecycleSystemResourcesStatusViewModel>();
    }
}
