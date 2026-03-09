using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.ViewModels;

public interface IModListEditorViewModel : IWidgetViewModel
{
    Guid Id { get; set; }
    Task LoadAsync(bool force = false);
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    Dictionary<PartId, List<ModEntity>>? Information { get; }
    string GetModName(ModEntity item);
}
