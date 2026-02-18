using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;

public class RelatedToResponseToConstraintTypeEntity : ICoreMapHandler<GameStartupParameterRelatedToResponse, GameStartupParameterConstraintTypeEntity>
{
    public GameStartupParameterConstraintTypeEntity Handler(GameStartupParameterRelatedToResponse data, ICoreMap alsoMap)
        => new GameStartupParameterConstraintTypeEntity()
        {
            Constraint = data.Constraint.ToLower() switch
            {
                "equals" => StartupParameterConstraintType.Equals,
                "greaterthan" => StartupParameterConstraintType.GreaterThan,
                "greaterthanorequal" => StartupParameterConstraintType.GreaterThanOrEqual,
                "lessthan" => StartupParameterConstraintType.LessThan,
                "lessthanorequal" => StartupParameterConstraintType.LessThanOrEqual,
                _ => StartupParameterConstraintType.NotEquals
            },
            Key = data.Key,
            Message = data.Key
        };
}
