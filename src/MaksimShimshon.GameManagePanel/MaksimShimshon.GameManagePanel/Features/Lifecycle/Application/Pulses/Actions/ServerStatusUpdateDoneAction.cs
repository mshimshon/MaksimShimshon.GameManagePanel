using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record ServerStatusUpdateDoneAction : IAction
{
    public ServerInfoEntity? ServerInfo { get; set; }
}
