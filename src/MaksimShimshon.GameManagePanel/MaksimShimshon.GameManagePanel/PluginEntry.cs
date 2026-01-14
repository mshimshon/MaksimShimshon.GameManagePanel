using CoreMap;
using LunaticPanel.Core;
using LunaticPanel.Core.Abstraction;
using LunaticPanel.Core.Abstraction.Circuit;
using MaksimShimshon.GameManagePanel.Features.Lifecycle;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer;
using MaksimShimshon.GameManagePanel.Features.Notification;
using MaksimShimshon.GameManagePanel.Features.SystemInfo;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.CQRS.Notifications;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Services;
using MaksimShimshon.GameManagePanel.Web.Pages.ViewModels;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel;


public class PluginEntry : PluginBase
{
    private IConfiguration _configuration = default!;
    private GameInfoConfiguration _gameInfoConfig = default!;
    private HeartbeatConfiguration _heartbeatConfig = default!;
    private RepositoryConfiguration _repositoryConfig = default!;

    protected override void LoadConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
        _gameInfoConfig = _configuration.GetSection("GameInfo")?.Get<GameInfoConfiguration>() ?? new();
        _heartbeatConfig = _configuration.GetSection("Heartbeat")?.Get<HeartbeatConfiguration>() ?? new();
        _repositoryConfig = _configuration.GetSection("Repositories")?.Get<RepositoryConfiguration>() ?? new();

    }
    protected override void RegisterPluginServices(IServiceCollection services, CircuitIdentity circuit)
    {
        services.AddScoped<CommandRunner>();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<IHeartbeatService, HeartbeatService>();
        services.AddScoped(sp => new PluginConfiguration(sp.GetRequiredService<IPluginConfiguration>())
        {
            GameInfo = _gameInfoConfig,
            Heartbeat = _heartbeatConfig,
            Repositories = _repositoryConfig
        });

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
        services.AddSystemInfoFeatureServices(_configuration);
    }

    protected override async Task BeforeRuntimeStart(IServiceProvider serviceProvider)
    {
        var medihater = serviceProvider.GetRequiredService<IMedihater>();
        await medihater.Publish<BeforeRuntimeInitNotification>(new());
        serviceProvider.RuntimeLinuxGameServerInitializer();

    }
}
