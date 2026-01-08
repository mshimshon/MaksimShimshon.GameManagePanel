using CoreMap;
using LunaticPanel.Core;
using LunaticPanel.Core.Abstraction.Circuit;
using MaksimShimshon.GameManagePanel.Features.Lifecycle;
using MaksimShimshon.GameManagePanel.Features.Notification;
using MaksimShimshon.GameManagePanel.Features.SystemInfo;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Services;
using MaksimShimshon.GameManagePanel.Web.Pages.ViewModels;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel;


public class PluginEntry : PluginBase
{
    private IConfiguration _configuration = default!;
    protected override void LoadConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void RegisterPluginServices(IServiceCollection services, CircuitIdentity circuit)
    {


        var config = new Configuration();
        config.GameInfo = _configuration.GetSection("GameInfo")?.Get<GameInfoConfiguration>();

        JObject jsonD = new();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<IHeartbeatService, HeartbeatService>();
        services.AddScoped(p => config);

        services.AddLogging();
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

        services.AddLifecycleFeatureServices();
        services.AddNotificationFeatureServices();
        services.AddSystemInfoFeatureServices();
    }
}
