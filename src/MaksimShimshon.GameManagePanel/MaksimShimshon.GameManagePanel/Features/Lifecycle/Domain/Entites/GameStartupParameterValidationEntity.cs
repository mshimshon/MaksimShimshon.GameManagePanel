using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public sealed record GameStartupParameterValidationEntity
{
    public string? UnitSuffix { get; init; }
    public string? UnitPrefix { get; init; }
    public int? MinLength { get; init; }
    public int? MaxLength { get; init; }
    public string? PatternValidation { get; init; }
    public int? Min { get; init; }
    public int? Max { get; init; }
    public ICollection<StartupParameterAllowedValue>? AllowedValues { get; init; }

}
