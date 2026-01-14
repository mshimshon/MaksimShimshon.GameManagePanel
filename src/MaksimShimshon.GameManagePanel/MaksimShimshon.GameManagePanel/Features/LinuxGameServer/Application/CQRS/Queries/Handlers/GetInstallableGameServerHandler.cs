using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries.Handlers;

internal class GetInstallableGameServerHandler : IRequestHandler<GetInstallableGameServerQuery, IReadOnlyDictionary<string, string>>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstallableGameServerHandler(ILinuxGameServerService linuxGameServerService)
    {
        _linuxGameServerService = linuxGameServerService;
    }
    public async Task<IReadOnlyDictionary<string, string>> Handle(GetInstallableGameServerQuery request, CancellationToken cancellationToken)
    {
        var result = await _linuxGameServerService.GetAvailableGames();
        return result.AsReadOnly();
    }
}
