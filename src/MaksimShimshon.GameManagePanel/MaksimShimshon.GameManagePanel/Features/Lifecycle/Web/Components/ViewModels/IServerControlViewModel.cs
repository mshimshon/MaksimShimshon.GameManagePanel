using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public interface IServerControlViewModel : IWidgetViewModel
{
    ServerState ServerState { get; }
    GameInfoState GameInfoState { get; }
    GameInfoEntity? GameInfo { get; }
    SystemInfoState SystemInfoState { get; }
    Task Start();
    Task UpdateState();
    Task Stop();
    bool IsRunning();
    bool IsStopped();
    bool IsRestarting();
    bool IsFailed();
    bool IsWaiting();
}
