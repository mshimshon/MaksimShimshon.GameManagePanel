using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.Dialogs.ViewModels;

public interface ICreateModListDialogViewModel : IWidgetViewModel
{
    string Name { get; set; }
    ModListState ModListState { get; }
    ModListLocalState ModListLocalState { get; }
    Guid Id { get; }
    bool IsAllowedComplete { get; }
    bool IsNotNullPass(string? str);
    bool IsMaxCharacterPass(string str, int maxLength);
    bool IsMinCharacterPass(string str, int minLength);
    bool IsAlphaNumericAndSpacePass(string str);
    Task CreateAsync();
    void OnParameterSet();
    bool IsBusy();
}
