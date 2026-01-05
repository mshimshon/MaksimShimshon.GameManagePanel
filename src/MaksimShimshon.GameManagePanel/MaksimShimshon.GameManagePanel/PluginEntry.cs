using CoreMap;
using LunaticPanel.Core;
using MaksimShimshon.GameManagePanel.Features.Lifecycle;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Pages;
using MaksimShimshon.GameManagePanel.Features.Notification;
using MaksimShimshon.GameManagePanel.Features.SystemInfo;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Services;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel;


public class PluginEntry : IPlugin
{
    private IConfiguration _configuration;

    public void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
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
        var config = new Configuration();
        config.GameInfo = _configuration.GetSection("GameInfo")?.Get<GameInfoConfiguration>();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<IHeartbeatService, HeartbeatService>();
        services.AddScoped(p => config);
        services.AddLifecycleFeatureServices();
        services.AddNotificationFeatureServices();
        services.AddSystemInfoFeatureServices();

        services.AddCoreMap(o => o.Scope = CoreMap.Enums.ServiceScope.Transient);
        services.AddStatePulseServices(c =>
        {
            c.DispatchOrderBehavior = StatePulse.Net.Configuration.DispatchOrdering.ReducersFirst;
            c.PulseTrackingPerformance = StatePulse.Net.Configuration.PulseTrackingModel.BlazorServerSafe;
        });

        services.AddMedihaterServices(c =>
        {
            c.Performance = MedihatR.Configuraions.Enums.PipelinePerformance.DynamicMethods;
            c.NotificationFireMode = MedihatR.Configuraions.Enums.PipelineNotificationFireMode.FireAndForget;
            c.CachingMode = MedihatR.Configuraions.Enums.PipelineCachingMode.EagerCaching;
        });
        services.AddScoped<HomeViewModel>();
    }
}
