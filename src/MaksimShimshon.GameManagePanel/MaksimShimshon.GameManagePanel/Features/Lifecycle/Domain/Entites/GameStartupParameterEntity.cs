using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

public record GameStartupParameterEntity
{
    // Key Value Object includes ENum of Type p
    public StartupParameter Key { get; init; } = default!;
    public string Label { get; init; } = default!;
    public string Description { get; init; } = default!;
    public bool Required { get; init; }
    public bool Editable { get; init; }
    public string? DefaultValue { get; init; }
    public string Category { get; init; } = default!;
    public string? Warning { get; init; }
    public GameStartupParameterValidationEntity? Validation { get; init; }
    public GameStartupParameterConstraintTypeEntity? RelatedTo { get; init; }
}
