using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto.Mapping;

internal class ModListEntityToModListResponse : ICoreMapHandler<ModListEntity, ModListResponse>
{
    //  TODO : Improve Dictionary Conversion
    public ModListResponse Handler(ModListEntity data, ICoreMap alsoMap)
    {
        var mods =
    data.Mods.ToDictionary(p => p.Key.Id, p => alsoMap.MapEach(p.Value.ToList()).To<ModResponse>().ToList());
        return new()
        {
            Id = data.Descriptor.Id,
            Name = data.Descriptor.Name,
            Mods = mods
        };
    }
}
