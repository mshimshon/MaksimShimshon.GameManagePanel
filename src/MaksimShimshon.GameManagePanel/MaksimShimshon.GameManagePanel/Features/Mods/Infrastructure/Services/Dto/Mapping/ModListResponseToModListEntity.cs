using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto.Mapping;

internal class ModListResponseToModListEntity : ICoreMapHandler<ModListResponse, ModListEntity>
{
    public ModListEntity Handler(ModListResponse data, ICoreMap alsoMap)
    {
        var mods =
            data.Mods.ToDictionary(
                p => new PartId(p.Key),
            p => alsoMap.MapEach(p.Value).To<ModEntity>());

        return new ModListEntity(new ModListDescriptor(data.Id, data.Name), mods);
    }
}
