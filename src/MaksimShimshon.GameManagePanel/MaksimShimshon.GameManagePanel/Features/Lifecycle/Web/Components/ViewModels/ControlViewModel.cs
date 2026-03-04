using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;


internal class ControlViewModel : WidgetViewModelBase, IServerControlViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;
    private readonly IStateAccessor<ServerState> _stateAccessor;

    public ServerState ServerState => _statePulse.StateOf<ServerState>(() => this, UpdateParentChanges);
    //public ServerState ServerState => _stateAccessor.State;
    public SystemInfoState SystemInfoState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateParentChanges);
    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateParentChanges);

    public GameInfoEntity? GameInfo => GameInfoState?.GameInfo;

    public ControlViewModel(IStatePulse statePulse, ICrazyReport crazyReport, IStateAccessor<ServerState> stateAccessor)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        _stateAccessor = stateAccessor;
        //_stateAccessor.OnStateChangedNoDetails += (_, e) => { _ = UpdateState(); };
        _crazyReport.SetModule<ControlViewModel>(LifecycleModule.ModuleName);
        _crazyReport.ReportInfo("Constructed");
    }

    public async Task UpdateState()
    {
        _crazyReport.ReportError("{0} Received an Update on Circuit", nameof(ServerState));
        await UpdateChanges();

    }

    public async Task UpdateState2()
    {
        _crazyReport.ReportError("{0} Received an Update 2 on Circuit", nameof(ServerState));
        await UpdateChanges();

    }
    public async Task Test()
    {
        _ = UpdateChanges();
    }

    public async Task Start()
    {
        IsLoading = true;
        await _statePulse.Dispatcher.Prepare<ServerStartAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task Stop()
    {
        IsLoading = true;
        await _statePulse.Dispatcher.Prepare<ServerStopAction>().DispatchAsync();
        IsLoading = false;
    }
    protected override bool GetStateLoadingStatus() => IsWaiting();
    public bool IsRunning() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsStopped() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Stopped;
    public bool IsRestarting() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Running;
    public bool IsFailed() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == Status.Failed;
    public bool IsWaiting() => ServerState.ServerInfo == default || ServerState.ServerInfo.Status == Status.Unknown;
}
