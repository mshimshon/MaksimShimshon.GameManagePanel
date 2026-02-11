using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands.Handlers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto.Mapping;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer;

public static class LinuxGameServerExt
{
    public static void AddLinuxGameServerFeatureServices(this IServiceCollection services,
        IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
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

        services.AddStatePulseService<UpdateProgressStateFromDiskDoneAction>();
        services.AddStatePulseService<UpdateProgressStateFromDiskAction>();
        services.AddStatePulseService<UpdateProgressStateFromDiskEffect>();
        services.AddStatePulseService<UpdateProgressStateFromDiskDoneReducer>();

        services.AddStatePulseService<UpdateInstalledGameServerAction>();
        services.AddStatePulseService<UpdateInstalledGameServerDoneAction>();
        services.AddStatePulseService<UpdateInstalledGameServerDoneReducer>();
        services.AddStatePulseService<UpdateInstalledGameServerEffect>();


        services.AddMedihaterRequestHandler<InstallGameServerCommand, InstallGameServerHandler, GameServerInfoEntity?>();
        services.AddMedihaterRequestHandler<GetInstallationProgressQuery, GetInstallationProgressHandler, GameServerInstallProcessModel?>();
        services.AddMedihaterRequestHandler<GetInstalledGameQuery, GetInstalledGameHandler, GameServerInfoEntity?>();

        services.AddScoped<ISetupProcessViewModel, SetupProcessViewModel>();
        services.AddScoped<IWidgetServerSetupViewModel, WidgetServerSetupViewModel>();

        services.AddScoped<IGitService, GitService>();
        services.AddScoped<ILinuxGameServerService, LinuxGameServerService>();
        services.AddCoreMapHandler<InstallationProgressToInstallationProcessModel>();
        services.AddCoreMapHandler<InstallationStateToGameServerInfoEntity>();
        if (isMaster)
        {
            var watchUpdate = FileWatchEvents.Updated;
            var watchCreation = FileWatchEvents.Created;
            var watchRemoval = FileWatchEvents.Removed;
            services.AddMasterStateFileWatcherService<UpdateInstalledGameServerAction>(
                c =>
                {
                    string path = c.GetConfigBase(LinuxGameServerModule.ModuleName);
                    Console.WriteLine(path);
                    return path;
                },
                "installation_state.json",
                [watchUpdate, watchCreation, watchRemoval]);

            services.AddMasterStateFileWatcherService<UpdateProgressStateFromDiskAction>(
                c => c.GetConfigBase(LinuxGameServerModule.ModuleName),
                "installation_progress_state.json",
                [watchUpdate, watchCreation, watchRemoval]);
        }


    }

    public static async Task RuntimeLinuxGameServerInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        await DownloadAvailableGames(
                serviceProvider.GetRequiredService<IGitService>(),
                serviceProvider.GetRequiredService<PluginConfiguration>()
            );

        IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();
        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnBeforeRuntimeInitialization);
        serviceProvider.LoadWatcher<UpdateInstalledGameServerAction>();
        serviceProvider.LoadWatcher<UpdateProgressStateFromDiskAction>();

    }
    public static async Task DownloadAvailableGames(IGitService gitService, PluginConfiguration pluginConfig)
    {
        Console.WriteLine($"Downloading Available Games");
        await gitService.CloneAsync(pluginConfig.Repositories.GitGameServerScriptRepository, "available_games");

    }
}
