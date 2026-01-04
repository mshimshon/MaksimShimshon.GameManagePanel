using GameServerManager.Core.Abstractions.Plugin;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;

public class LifecycleEventMessageNoData : PluginEventBusMessageRequestNoData
{
    public LifecycleEventMessageNoData(LifecycleEvents eventType) : base(eventType.ToString()) { }
    public LifecycleEventMessageNoData(string baseEventId, LifecycleEvents eventType) : base(baseEventId, eventType.ToString()) { }
}
