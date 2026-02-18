using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;

public class ValidationResponseToValidationEntity : ICoreMapHandler<GameStartupParameterValidationResponse, GameStartupParameterValidationEntity>
{
    public GameStartupParameterValidationEntity Handler(GameStartupParameterValidationResponse data, ICoreMap alsoMap)
        => new()
        {
            Max = data.Max,
            Min = data.Min,
            MaxLength = data.MaxLength,
            MinLength = data.MinLength,
            UnitPrefix = data.UnitPrefix,
            UnitSuffix = data.UnitSuffix,
            PatternValidation = data.Pattern,
            AllowedValues = data.AllowedValues != default ? alsoMap.MapEach(data.AllowedValues).To<StartupParameterAllowedValue>() : default
        };
}
