using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Handlers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Configuration;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Hooks.Components.ViewModels;
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

        services.AddScoped<ISetupProcessViewModel, SetupProcessViewModel>();
        services.AddScoped<IWidgetServerSetupViewModel, WidgetServerSetupViewModel>();

        services.AddMedihaterRequestHandler<GetInstallableGameServerQuery, GetInstallableGameServerHandler, IReadOnlyDictionary<string, string>>();
        services.AddMedihaterNotificationHandler<BeforeRuntimeInitNotification, LoadInstallationStateHandler>();

    }
}
