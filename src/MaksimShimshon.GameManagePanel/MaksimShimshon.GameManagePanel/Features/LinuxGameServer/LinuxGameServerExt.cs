using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer;

public static class LinuxGameServerExt
{
    public static void AddLinuxGameServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var config =
            configuration.GetSection("LinuxGameServer")?.Get<LinuxGameServerConfiguration>() ??
            new LinuxGameServerConfiguration();

        services.AddScoped(sp => config);
        services.AddStatePulseService<GameServerInstalledAction>();
        services.AddStatePulseService<InstallGameServerAction>();
        services.AddStatePulseService<GameServerInstallFailedAction>();
        services.AddStatePulseService<InstallGameServerActionEffect>();
        services.AddStatePulseService<InstallGameServerActionReducer>();
        services.AddStatePulseService<GameServerInstalledReducer>();
        services.AddStatePulseService<GameServerInstallFailedReducer>();
        services.AddStatePulseService<InstallationState>();
        services.AddStatePulseService<GameServerInstallStateLoadedAction>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallReducer>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallAction>();
        services.AddStatePulseService<GameRepositoryState>();
        services.AddStatePulseService<RepositoryDownloadStartedAction>();
        services.AddStatePulseService<RepositoryDownloadStartedReducer>();
        services.AddStatePulseService<ReceivingUpdatedInstallStateAction>();
        services.AddStatePulseService<ReceivingInstallationStateReducer>();

        services.AddScoped<ISetupProcessViewModel, SetupProcessViewModel>();
        services.AddScoped<IWidgetServerSetupViewModel, WidgetServerSetupViewModel>();

        services.AddScoped<IGitService, GitService>();
        services.AddScoped<ILinuxGameServerService, LinuxGameServerService>();
    }

    public static async Task RuntimeLinuxGameServerInitializer(this IServiceProvider serviceProvider)
    {
        Console.WriteLine($"{LinuxGameServerModule.ModuleName} Runtime Setup");
        await DownloadAvailableGames(
                serviceProvider.GetRequiredService<IGitService>(),
                serviceProvider.GetRequiredService<PluginConfiguration>(),
                serviceProvider.GetRequiredService<IDispatcher>()
            );

        IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();
        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnBeforeRuntimeInitialization);
    }
    public static async Task DownloadAvailableGames(IGitService gitService, PluginConfiguration pluginConfig, IDispatcher dispatcher)
    {
        Console.WriteLine($"Downloading Available Games");
        await gitService.CloneAsync(pluginConfig.Repositories.GitGameServerScriptRepository, "available_games");

    }
}
