using GameServerManager.Core.Abstractions.Plugin;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;

public class LifecycleEventMessage : PluginEventBusMessageRequest
{
    public LifecycleEventMessage(LifecycleEvents eventType, object data) : base(eventType.ToString(), data){}
    public LifecycleEventMessage(string baseEventIdTarget, LifecycleEvents eventType, object data) : base(baseEventIdTarget, eventType.ToString(), data){ }
}
