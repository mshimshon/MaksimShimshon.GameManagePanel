using CoreMap;
using LunaticPanel.Core;
using MaksimShimshon.GameManagePanel.Features.Lifecycle;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel;


public class PluginEntry : IPlugin
{
    public void Configure(IConfiguration configuration)
    {

    }
    public void Disable()
    {

    }
    public void Enable()
    {

    }
    public void Initialize()
    {

    }

    public void RegisterServices(IServiceCollection services)
    {
        services.AddLifecycleFeatureServices();
        services.AddNotificationFeatureServices();
        services.AddCoreMap(o => o.Scope = CoreMap.Enums.ServiceScope.Transient, [typeof(PluginEntry)]);
        services.AddStatePulseServices(c =>
        {
            c.ScanAssemblies = [typeof(PluginEntry).Assembly];
            c.DispatchOrderBehavior = StatePulse.Net.Configuration.DispatchOrdering.ReducersFirst;
        });

        services.AddMedihaterServices(c =>
        {
            c.Performance = MedihatR.Configuraions.Enums.PipelinePerformance.DynamicMethods;
            c.NotificationFireMode = MedihatR.Configuraions.Enums.PipelineNotificationFireMode.FireAndForget;
            c.CachingMode = MedihatR.Configuraions.Enums.PipelineCachingMode.EagerCaching;
            c.AssembliesScan = [typeof(PluginEntry)];
        });
        services.AddScoped<LifecycleViewModel>();
    }
}
