using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;

public class ServerSystemInfoUpdatedReducer : IReducer<SystemInfoState, SystemInfoUpdatedAction>
{
    private readonly IEventBus _eventBus;

    public ServerSystemInfoUpdatedReducer(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task<SystemInfoState> ReduceAsync(SystemInfoState state, SystemInfoUpdatedAction action)
    {
        var result = state with { SystemInfo = action.SystemInfo, LastUpdate = DateTime.UtcNow };
        await _eventBus.PublishDataAsync(SystemInfoKeys.Events.OnStateUpdate, result);
        return result;
    }
}
