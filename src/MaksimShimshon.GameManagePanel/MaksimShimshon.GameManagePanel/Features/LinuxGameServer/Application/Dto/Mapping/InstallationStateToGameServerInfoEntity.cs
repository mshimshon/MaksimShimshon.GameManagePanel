using CoreMap;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Domain.Entities;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto.Mapping;

internal class InstallationStateToGameServerInfoEntity : ICoreMapHandler<InstallationStateDto, GameServerInfoEntity>
{
    public GameServerInfoEntity Handler(InstallationStateDto data, ICoreMap alsoMap)
        => new GameServerInfoEntity()
        {
            Id = data.Id,
            InstallDate = data.InstallDate
        };
}
