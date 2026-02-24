using CoreMap;
using LunaticPanel.Core;
using LunaticPanel.Core.Abstraction;
using LunaticPanel.Core.Abstraction.Circuit;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.Lifecycle;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer;
using MaksimShimshon.GameManagePanel.Features.Notification;
using MaksimShimshon.GameManagePanel.Features.SystemInfo;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat;
using MaksimShimshon.GameManagePanel.Kernel.Middlewares.StatePulse;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
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
    private HeartbeatConfiguration _heartbeatConfig = default!;
    private RepositoryConfiguration _repositoryConfig = default!;

    protected override void LoadConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
        _heartbeatConfig = _configuration.GetSection("Heartbeat")?.Get<HeartbeatConfiguration>() ?? new();
        _repositoryConfig = _configuration.GetSection("Repositories")?.Get<RepositoryConfiguration>() ?? new();

    }
    private IServiceCollection? _statePulseStatesRedirectionSingleton;
    private List<ServiceDescriptor>? _statePulseStatesSingleton;
    private static Guid MasterId { get; set; }
    protected override void RegisterPluginServices(IServiceCollection services, CircuitIdentity circuit)
    {
        if (circuit.IsMaster) MasterId = circuit.CircuitId;
        Console.WriteLine($"Is {circuit.CircuitId} DI Master? {(circuit.IsMaster)}");

        services.AddTransient<ILinuxLockFileController, LinuxLockFileController>();
        services.AddStatePulseService<DispatchErrorMiddleware>();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<IHeartbeatService, HeartbeatService>();
        services.AddScoped(sp => new PluginConfiguration(sp.GetRequiredService<IPluginConfiguration>(), sp.GetRequiredService<ICrazyReport>())
        {
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

        services.AddLifecycleFeatureServices(circuit.IsMaster);
        services.AddNotificationFeatureServices();
        services.AddLinuxGameServerFeatureServices(_crossCircuitSingletonProvider!, _configuration, circuit.IsMaster);
        services.AddSystemInfoFeatureServices(_configuration);
        services.AddTransient<ICrazyReport, CrazyReport>();
        // Make Singleton State cross circuit
        if (_statePulseStatesRedirectionSingleton == default)
        {
            _statePulseStatesRedirectionSingleton = new ServiceCollection();
            _statePulseStatesSingleton = new();
            foreach (var d in services)
            {
                if (d.ServiceType.IsGenericTypeDefinition)
                    continue;

                if (d.Lifetime != ServiceLifetime.Singleton)
                    continue;

                if (!d.ServiceType.IsGenericType ||
                    d.ServiceType.GetGenericTypeDefinition() != typeof(IStateAccessor<>))
                    continue;
                _statePulseStatesSingleton.Add(d);
                _statePulseStatesRedirectionSingleton.AddSingleton(d.ServiceType, sp => _crossCircuitSingletonProvider!.GetRequiredService(d.ServiceType));
            }
        }
        if (_statePulseStatesRedirectionSingleton != default)
            foreach (var item in _statePulseStatesRedirectionSingleton)
                services.Add(item);
    }
    protected override void RegisterPluginSingletonServices(IServiceCollection services, CircuitIdentity circuit)
    {
        if (_statePulseStatesSingleton != default)
        {
            foreach (var d in _statePulseStatesSingleton)
                services.Add(d);
        }

    }

    protected override async Task BeforeRuntimeStart(IPluginContextService pluginContext)
    {
        var sp = pluginContext.GetRequired<IServiceProvider>();
        IEventBus eventBus = sp.GetRequiredService<IEventBus>();
        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnBeforeRuntimeInitialization);
        await sp.RuntimeLifecycleInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeLinuxGameServerInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeSystemInfoFeatureInitializer(MasterId == pluginContext.CircuitId);
        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnAfterRuntimeInitialization);
    }
}
