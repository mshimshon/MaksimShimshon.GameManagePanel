using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.CQRS.Notifications;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Notifications.Handlers;

internal class LoadAvailableGameServerToInstallHandler : INotificationHandler<BeforeRuntimeInitNotification>
{
    private readonly ILinuxGameServerService _linuxGameServerService;
    private readonly IDispatcher _dispatcher;

    public LoadAvailableGameServerToInstallHandler(ILinuxGameServerService linuxGameServerService, IDispatcher dispatcher)
    {
        _linuxGameServerService = linuxGameServerService;
        _dispatcher = dispatcher;
    }
    public async Task Handle(BeforeRuntimeInitNotification notification, CancellationToken cancellationToken)
    {
        var result = await _linuxGameServerService.GetAvailableGames(cancellationToken);
        await _dispatcher.Prepare<PopulateAvailableGamesForInstallAction>(result).DispatchAsync();
    }
}
