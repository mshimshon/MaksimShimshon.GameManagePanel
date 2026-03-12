using LunaticPanel.Core.Abstraction.Widgets.Enum;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.Dialogs.ViewModels;
using StatePulse.Net;
using System.Text.RegularExpressions;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components.Dialogs;

internal class CreateModListDialogViewModel : ICreateModListDialogViewModel
{

    public bool IsLoading { get; set; } = false;

    public Guid Id { get; set; } = Guid.NewGuid();
    public bool FirstRenderCompleted { get; set; } = true;

    public event Func<SpreadChangeOption, Task>? SpreadChanges;

    private const string ALPHA_NUMERIC_N_SPACES = @"^[A-Za-z0-9 ]+$";
    private readonly IStatePulse _statePulse;
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public bool IsAllowedComplete { get; set; }
    public string Name { get; set; }
    public bool IsBusy()
        => IsLoading || ModListState.IsLoadingAvailable || ModListLocalState.IsCreationLoading;

    public void OnParameterSet()
    {
        IsAllowedComplete = ModListState.Available.Any(p => p.Id == Id);
    }

    private async Task UpdateChanges()
    {
        SpreadChanges?.Invoke(SpreadChangeOption.TouchMyComponentOnly);
    }
    public CreateModListDialogViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }

    public bool IsNotNullPass(string? str) => !string.IsNullOrWhiteSpace(str);
    public bool IsMaxCharacterPass(string str, int maxLength) => str.Length <= maxLength;
    public bool IsMinCharacterPass(string str, int minLength) => str.Length > minLength;
    public bool IsAlphaNumericAndSpacePass(string str) => Regex.IsMatch(str, ALPHA_NUMERIC_N_SPACES);
    public async Task CreateAsync()

    {

    }
}
