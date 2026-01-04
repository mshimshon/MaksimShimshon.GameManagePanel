using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses.Mapping;

public class StatusGameInfoResponseToGameInfoEntity : ICoreMapHandler<GameInfoResponse, GameInfoEntity>
{
    public GameInfoEntity Handler(GameInfoResponse data, ICoreMap alsoMap) => new GameInfoEntity()
    {
        ManualModUpload = data.ManualModUpload,
        Modding = data.Modding,
        Name = data.Name,
        SteamInfo = data.Steam && !string.IsNullOrWhiteSpace(data.SteamAppId) ? new SteamGameId(data.SteamAppId, data.Modding && data.Workshop) : default,
        StartupParameters = alsoMap.MapEach(data.Parameters).To<GameStartupParameterEntity>()
    };
}
