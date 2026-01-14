using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Services;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProcessViewModel : WidgetViewModelBase, ISetupProcessViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly IMedihater _medihater;
    private readonly IDispatcher _dispatcher;
    private readonly PluginConfiguration _pluginConfiguration;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);

    public string KeyGame { get; set; }

    private IReadOnlyDictionary<string, string> _availableInstallGame = new Dictionary<string, string>().AsReadOnly();
    public IReadOnlyDictionary<string, string> AvailableInstallGames => _availableInstallGame;

    public SetupProcessViewModel(IStatePulse statePulse, IMedihater medihater, PluginConfiguration pluginConfiguration)
    {
        _statePulse = statePulse;
        _medihater = medihater;
        _dispatcher = statePulse.Dispatcher;
        _pluginConfiguration = pluginConfiguration;

    }

    public async Task InstallAsync()
    {
        IsLoading = true;
        await _dispatcher.Prepare<InstallGameServerAction>()
            .With(p => p.Id, KeyGame)
            .With(p => p.DisplayName, AvailableInstallGames[KeyGame])
            .DispatchAsync();
    }

    public async Task InitializeAsync()
    {
        try
        {
            var request = new GetInstallableGameServerQuery();
            _availableInstallGame = await _medihater.Send(request);
        }
        catch { }

    }
}
