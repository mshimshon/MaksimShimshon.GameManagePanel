using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components.ViewModels;

public class WidgetServerControlViewModel : WidgetViewModelBase, IWidgetServerControlViewModel
{

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public LifecycleServerState ServerState => _statePulse.StateOf<LifecycleServerState>(() => this, UpdateChanges);
    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, UpdateChanges);

    public ServerInfoEntity? ServerInfo => ServerState.ServerInfo;
    public ServerTransition Transition => ServerState.Transition;

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    public WidgetServerControlViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    public async Task Start()
    {
        IsLoading = true;
        await _dispatcher.Prepare<LifecycleServerStartAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task Stop()
    {
        IsLoading = true;
        await _dispatcher.Prepare<LifecycleServerStopAction>().DispatchAsync();
        IsLoading = false;
    }
    public bool IsRunning() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsStopped() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Stopped;
    public bool IsRestarting() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsFailed() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Failed;
    public bool IsWaiting() => ServerState.ServerInfo == default || ServerState.ServerInfo.Status == Status.Unknown;
}
