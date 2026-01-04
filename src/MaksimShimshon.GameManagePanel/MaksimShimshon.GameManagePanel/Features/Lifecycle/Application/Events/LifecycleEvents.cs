using System;
using System.Collections.Generic;
using System.Text;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Events;

public enum LifecycleEvents
{
    ServerStartBegin,
    ServerStartFailed,
    ServerStartSuccess,
    ServerStartFinish,

    ServerStatusChanged,

    ServerRestartBegin,
    ServerRestartFailed,
    ServerRestartSuccess,
    ServerRestartFinish,

    ServerStopBegin,
    ServerStopFailed,
    ServerStopSuccess,
    ServerStopFinish,

    UpdateStartupParametersBegin,
    UpdateStartupParametersFailed,
    UpdateStartupParametersSuccess,
    UpdateStartupParametersFinish,

    GameInfoUpdated
}
