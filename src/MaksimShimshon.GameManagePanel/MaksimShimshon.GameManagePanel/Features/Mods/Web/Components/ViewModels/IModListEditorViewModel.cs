using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;

public interface IModListEditorViewModel : IWidgetViewModel
{
    Guid InitialId { get; set; }
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    Dictionary<PartId, List<ModEntity>>? Information { get; }
    string GetModName(ModEntity item);
    void MoveTo(PartId partId, ModEntity toMove, int targetIndex);
    void AddTo(PartId partId, ModEntity toAdd);
    void RemoveFrom(PartId partId, ModEntity toRemove);
}
