using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Hooks.UI.Components.ViewModels;

public interface IWidgetServerControlViewModel : IWidgetViewModel
{
    public ServerInfoEntity? ServerInfo { get; }
    public ServerTransition Transition { get; }
    public GameInfoEntity? GameInfo { get; }

    public Task Start();
    public Task Stop();
    public bool IsRunning();
    public bool IsStopped();
    public bool IsRestarting();
    public bool IsFailed();
    public bool IsWaiting();
}
