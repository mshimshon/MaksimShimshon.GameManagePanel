using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;

internal record PopulateAvailableGamesForInstallAction(Dictionary<string, string> AvailableGameServer) : IAction
{

}
