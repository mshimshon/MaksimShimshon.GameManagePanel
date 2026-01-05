using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle;

public static class LifecycleServiceExt
{
    public static void AddLifecycleFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<LifecycleViewModel>();
        services.AddScoped<LifecycleStartupParameterViewModel>();
        services.AddTransient<LifecycleStartupParameterFieldViewModel>();
        services.AddScoped<CommandRunner>();
    }
}
