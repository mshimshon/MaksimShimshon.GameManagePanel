using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;

internal class LifecycleServerGameInfoUpdateEffect : IEffect<LifecycleServerGameInfoUpdateAction>
{
    private readonly IMedihater _medihater;
    private readonly ICrazyReport _crazyReport;

    public LifecycleServerGameInfoUpdateEffect(IMedihater medihater, ICrazyReport crazyReport)
    {
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<LifecycleServerGameInfoUpdateEffect>(LifecycleModule.ModuleName);
    }
    public async Task EffectAsync(LifecycleServerGameInfoUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetGameInfoQuery();
        var gameInfo = await _medihater.Send(exec);
        _crazyReport.ReportInfo("gameInfo is {0}", gameInfo?.ToString() ?? null);
        if (gameInfo != default)
        {
            //TODO: MONITOR CONSISTENCY OF STATE AS TWO ACTION CHANGES THE SAME STATE DIFFERENT PROPS.
            await dispatcher.Prepare<LifecycleServerGameInfoUpdateDoneAction>()
                .With(p => p.GameInfo, gameInfo)
                .DispatchAsync();
            await dispatcher.Prepare<LifecycleFetchStartupParametersAction>()
                .DispatchAsync();
        }

    }
}
