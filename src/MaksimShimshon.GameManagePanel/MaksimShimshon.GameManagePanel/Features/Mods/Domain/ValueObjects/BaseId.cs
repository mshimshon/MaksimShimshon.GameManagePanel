using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions.Extensions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

public abstract record BaseId
{
    public string Id { get; }
    protected BaseId(string id)
    {
        Id = id;
        ValidateId<BaseId>();
    }
    protected virtual void ValidateId<TOrigin>()
    {
        Id.ThrowIfNullOrWhiteSpace<TOrigin>(nameof(Id));
        if (Id.Any(char.IsWhiteSpace))
            throw new PartIdForbiddenCharactersException("Spaces");
    }
}

public abstract record BaseId<TOrigin> : BaseId
{
    protected BaseId(string id) : base(id)
    {
        Id.ThrowIfNullOrWhiteSpace<TOrigin>(nameof(Id));
    }

    protected sealed override void ValidateId<T>()
    {
        base.ValidateId<TOrigin>();
    }
}
