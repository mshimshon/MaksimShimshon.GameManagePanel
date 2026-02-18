using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;

public class GameStartupParameterResponseToStartupParameter : ICoreMapHandler<GameStartupParameterResponse, StartupParameter>
{
    public StartupParameter Handler(GameStartupParameterResponse data, ICoreMap alsoMap)
        => new StartupParameter(data.Key, data.Type.ToLower() switch
        {
            "decimal" => StartupParameterType.Decimal,
            "bool" => StartupParameterType.Bool,
            "list" => StartupParameterType.List,
            "string" => StartupParameterType.String,
            _ => StartupParameterType.Int
        });
}
