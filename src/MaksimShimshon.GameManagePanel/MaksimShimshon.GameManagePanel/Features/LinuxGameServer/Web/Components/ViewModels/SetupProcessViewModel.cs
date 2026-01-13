using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Services;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProcessViewModel : WidgetViewModelBase, ISetupProcessViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly PluginConfiguration _pluginConfiguration;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);

    public string KeyGame { get; set; }

    private readonly IReadOnlyDictionary<string, string> _availableInstallGame = new Dictionary<string, string>().AsReadOnly();
    public IReadOnlyDictionary<string, string> AvailableInstallGames => _availableInstallGame;

    public SetupProcessViewModel(IStatePulse statePulse, PluginConfiguration pluginConfiguration)
    {
        _statePulse = statePulse;
        _pluginConfiguration = pluginConfiguration;

    }
}
