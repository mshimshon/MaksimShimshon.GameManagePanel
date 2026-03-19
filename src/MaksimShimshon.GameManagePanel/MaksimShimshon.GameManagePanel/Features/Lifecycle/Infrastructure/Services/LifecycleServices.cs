using CoreMap;
using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services;

internal class LifecycleServices : ILifecycleServices, IGameInfoService, IStartupParameterService
{
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICoreMap _coreMap;
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ICrazyReport _crazyReport;
    private const string SERVER_CONTROL_FOLDER = "server_control";
    private const string SERVER_CONTROL_COMMON_FOLDER = "common";
    private const string SERVER_CONTROL_COMMON_STATUS_FILE = "server_status.sh";
    private const string SERVER_CONTROL_COMMON_START_FILE = "start_server.sh";
    private const string SERVER_CONTROL_COMMON_STOP_FILE = "stop_server.sh";
    private const string USER_DEF_STARTUP_PARAM_FILE = "user_defined_startup_params.json";
    private const string GAMEINFO_FILE = "game_info.json";
    private const string USERNAME = "lgsm";

    private readonly JsonSerializerOptions _jsonSerializerConfiguration;
    private string? _rawGameInfo;
    public LifecycleServices(ILinuxCommand linuxCommand, ICoreMap coreMap, PluginConfiguration pluginConfiguration, ICrazyReport<LifecycleServices> crazyReport)
    {
        _linuxCommand = linuxCommand;
        _coreMap = coreMap;
        _pluginConfiguration = pluginConfiguration;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LifecycleKeys.ModuleName);
        _jsonSerializerConfiguration = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
    }
    public async Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default)
    {
        string file = _pluginConfiguration.GetUserConfigFor(LifecycleKeys.ModuleName, USER_DEF_STARTUP_PARAM_FILE);
        _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
        if (!File.Exists(file)) return new();
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<List<GameStartupParameterKeyValueResponse>>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return new();
            return result.ToDictionary(p => p.Key, p => p.Value); ;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
            throw new WebServiceException($"Unable to load {USER_DEF_STARTUP_PARAM_FILE}", ex);
        }
    }

    public async Task<string?> GetRawGameInfoAsync(CancellationToken ct = default)
    {
        try
        {
            if (_rawGameInfo != default)
                return _rawGameInfo;

            string file = _pluginConfiguration.GetUserBashFor(LifecycleKeys.ModuleName, [SERVER_CONTROL_FOLDER], GAMEINFO_FILE);
            _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
            if (!File.Exists(file)) return default;
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            _rawGameInfo = jsonString;
            return _rawGameInfo;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
            throw new WebServiceException($"Unable to load {GAMEINFO_FILE}", ex);
        }
    }

    public async Task<GameInfoEntity?> LoadGameInfoAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var jsonString = await GetRawGameInfoAsync(cancellationToken);
            if (jsonString == default) return default;
            var result = JsonSerializer.Deserialize<GameInfoResponse>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameInfoEntity>();
            return entity;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
            throw new WebServiceException($"Unable to load {GAMEINFO_FILE}", ex);
        }
    }

    public Task ServerRestartAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public async Task ServerStartAsync(CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetUserBashFor(LifecycleKeys.ModuleName, [SERVER_CONTROL_FOLDER, SERVER_CONTROL_COMMON_FOLDER], SERVER_CONTROL_COMMON_START_FILE);
        await _linuxCommand.BuildBash(script).AsUser(USERNAME).ExecAsync(cancellationToken);
    }

    public async Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetUserBashFor(LifecycleKeys.ModuleName, [SERVER_CONTROL_FOLDER, SERVER_CONTROL_COMMON_FOLDER], SERVER_CONTROL_COMMON_STATUS_FILE);
        var result = await _linuxCommand.BuildBash(script).Sudo().ExecAndReadAs<StatusResponse>((str) => new() { Completed = false, Failure = str }, cancellationToken);
        if (!result.Completed)
            throw new WebServiceException("Failed unable to get server status"); //TODO: Localize
        var resultEntity = _coreMap.Map(result).To<ServerInfoEntity>();
        return resultEntity;
    }

    public async Task ServerStopAsync(CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetUserBashFor(LifecycleKeys.ModuleName, [SERVER_CONTROL_FOLDER, SERVER_CONTROL_COMMON_FOLDER], SERVER_CONTROL_COMMON_STOP_FILE);
        await _linuxCommand.BuildBash(script).AsUser(USERNAME).ExecAsync(cancellationToken);
    }

    public async Task UpdateStartupParameterAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetBashFor(LifecycleKeys.ModuleName, "update_startup_parameters.sh", key, value);
        var result = await _linuxCommand.BuildBash(script).Sudo().ExecAndReadAs<ScriptResponse>((str) => new() { Completed = false, Failure = str }, cancellationToken);
        if (!result.Completed) throw new WebServiceException("Update of the startup parameter failed"); //TODO: Localize
    }
}
