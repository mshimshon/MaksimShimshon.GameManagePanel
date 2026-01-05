using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo;

public static class SystemInfoServiceExt
{
    public static void AddSystemInfoFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<LifecycleSystemResourcesStatusViewModel>();
        services.AddScoped<CommandRunner>();
    }
}
