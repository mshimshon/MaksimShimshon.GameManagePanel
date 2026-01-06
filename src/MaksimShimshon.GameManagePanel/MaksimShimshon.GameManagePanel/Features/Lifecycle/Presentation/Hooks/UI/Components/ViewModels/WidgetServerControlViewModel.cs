using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components.ViewModels;

public class WidgetServerControlViewModel : IWidgetServerControlViewModel
{
    private bool _loading = false;
    public bool IsLoading
    {
        get => _loading;
        private set
        {
            bool hasChanged = value != _loading;
            _loading = value;
            if (hasChanged)
                _ = SpreadChanges?.Invoke();
        }
    }

    public event Func<Task>? SpreadChanges;

    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public LifecycleServerState ServerState => _statePulse.StateOf<LifecycleServerState>(() => this, OnUpdate);
    public LifecycleGameInfoState GameInfoState => _statePulse.StateOf<LifecycleGameInfoState>(() => this, OnUpdate);

    public ServerInfoEntity? ServerInfo => ServerState.ServerInfo;
    public ServerTransition Transition => ServerState.Transition;

    public GameInfoEntity? GameInfo => GameInfoState.GameInfo;

    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
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
