using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerStatusUpdateEffect : IEffect<ServerStatusUpdateAction>
{
    private readonly IStateAccessor<ServerState> _stateAccessor;
    private readonly IMedihater _medihater;
    private readonly ICrazyReport _crazyReport;

    public ServerStatusUpdateEffect(
        IStateAccessor<ServerState> stateAccessor,
        IMedihater medihater, ICrazyReport crazyReport)
    {
        _stateAccessor = stateAccessor;
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<ServerGameInfoUpdateEffect>(LifecycleModule.ModuleName);
    }
    public async Task EffectAsync(ServerStatusUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetServerStatusQuery();
        var serverInfo = await _medihater.Send(exec);
        _crazyReport.ReportInfo("ServerInfoEntity = {0}", (serverInfo?.ToString() ?? "Null"));
        if (serverInfo != default && _stateAccessor.State.Transition != ServerTransition.Idle)
        {
            bool isStartingTransitionCompleted = _stateAccessor.State.Transition == ServerTransition.Starting && serverInfo.Status == Status.Running;
            bool isStoppedTransitionCompleted = _stateAccessor.State.Transition == ServerTransition.Stopping && serverInfo.Status == Status.Stopped;
            _crazyReport.ReportInfo("Server Info Transition Starting Done = {0}, Stopping Done = {0}", isStartingTransitionCompleted, isStoppedTransitionCompleted);

            if (isStartingTransitionCompleted || isStoppedTransitionCompleted)
                await dispatcher.Prepare<ServerStatusTransitionDoneAction>().Await().DispatchAsync();
        }


        await dispatcher.Prepare<ServerStatusUpdateDoneAction>().With(p => p.ServerInfo, serverInfo).DispatchAsync();

    }
}
