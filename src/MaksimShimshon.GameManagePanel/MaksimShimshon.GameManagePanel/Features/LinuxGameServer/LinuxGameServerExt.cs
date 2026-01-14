using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Handlers;
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
using MaksimShimshon.GameManagePanel.Kernel.CQRS.Notifications;
using MedihatR;
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

        services.AddScoped<ISetupProcessViewModel, SetupProcessViewModel>();
        services.AddScoped<IWidgetServerSetupViewModel, WidgetServerSetupViewModel>();

        services.AddScoped<IGitService, GitService>();
        services.AddScoped<ILinuxGameServerService, LinuxGameServerService>();

        services.AddMedihaterNotificationHandler<BeforeRuntimeInitNotification, LoadInstallationStateHandler>();

    }

    public static void RuntimeLinuxGameServerInitializer(this IServiceProvider serviceProvider)
    {
        DownloadAvailableGames(serviceProvider.GetRequiredService<IGitService>(), serviceProvider.GetRequiredService<PluginConfiguration>());
    }
    public static void DownloadAvailableGames(IGitService gitService, PluginConfiguration pluginConfig)
    {

        gitService
            .CloneAsync(pluginConfig.Repositories.GitGameServerScriptRepository, pluginConfig.GetReposFor(LinuxGameServerModule.ModuleName, "available_games"))
            .GetAwaiter()
            .GetResult();

    }
}
