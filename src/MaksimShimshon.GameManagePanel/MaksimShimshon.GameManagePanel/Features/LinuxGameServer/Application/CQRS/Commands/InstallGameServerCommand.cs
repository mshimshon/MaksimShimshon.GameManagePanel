using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands;

public record InstallGameServerCommand(string Id, string DisplayName) : IRequest
{
}
