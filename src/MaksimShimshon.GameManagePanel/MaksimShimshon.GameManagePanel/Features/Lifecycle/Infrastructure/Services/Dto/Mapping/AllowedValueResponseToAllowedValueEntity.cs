using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;

public class AllowedValueResponseToAllowedValueEntity : ICoreMapHandler<GameStartupParameterAllowedValueResponse, StartupParameterAllowedValue>
{
    public StartupParameterAllowedValue Handler(GameStartupParameterAllowedValueResponse data, ICoreMap alsoMap)
        => new(data.Value, data.Label);
}
