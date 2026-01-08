using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Engine.Keys.System;
using MaksimShimshon.GameManagePanel.Kernel.Heartbeat.Pulses.Actions;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Services.Hooks.Events;

[EventBusId(PluginKeys.Events.OnInitialize)]
public class HearbeatEventHook : IEventBusHandler
{
    private readonly IDispatcher _dispatcher;

    public HearbeatEventHook(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(IEventBusMessage evt)
    {
        await _dispatcher.Prepare<HeartbeatStartAction>().DispatchAsync();
    }
}
