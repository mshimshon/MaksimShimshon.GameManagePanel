using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;

public class ServerStatusUpdateDoneAction : IAction
{
    public ServerInfoEntity? ServerInfo { get; set; }
}
