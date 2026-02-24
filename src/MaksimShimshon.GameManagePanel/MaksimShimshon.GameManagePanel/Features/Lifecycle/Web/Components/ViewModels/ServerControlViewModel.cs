using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

internal class ServerControlViewModel : WidgetViewModelBase, IServerControlViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public ServerState ServerState => _statePulse.StateOf<ServerState>(() => this, UpdateChanges);
    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateParentChanges);

    public ServerInfoEntity? ServerInfo => ServerState.ServerInfo;
    public ServerTransition Transition => ServerState.Transition;

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    public ServerControlViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    public async Task Start()
    {
        IsLoading = true;
        await _dispatcher.Prepare<ServerStartAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task Stop()
    {
        IsLoading = true;
        await _dispatcher.Prepare<ServerStopAction>().DispatchAsync();
        IsLoading = false;
    }
    public bool IsRunning() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsStopped() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Stopped;
    public bool IsRestarting() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsFailed() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Failed;
    public bool IsWaiting() => ServerState.ServerInfo == default || ServerState.ServerInfo.Status == Status.Unknown;
}
