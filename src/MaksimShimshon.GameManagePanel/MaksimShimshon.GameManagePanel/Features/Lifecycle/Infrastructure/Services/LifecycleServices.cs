using CoreMap;
using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services;

internal class LifecycleServices : ILifecycleServices
{
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICoreMap _coreMap;
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ILogger<LifecycleServices> _logger;
    private readonly ICrazyReport _crazyReport;
    private const string SERVER_CONTROL_FOLDER = "server_control";
    private const string SERVER_CONTROL_COMMON_FOLDER = "common";
    private const string SERVER_CONTROL_COMMON_STATUS_FILE = "server_status.sh";
    private const string USER_DEF_STARTUP_PARAM_FILE = "user_defined_startup_params.json";
    private const string GAMEINFO_FILE = "game_info.json";

    private readonly JsonSerializerOptions _jsonSerializerConfiguration;

    public LifecycleServices(ILinuxCommand linuxCommand, ICoreMap coreMap, PluginConfiguration pluginConfiguration, ILogger<LifecycleServices> logger, ICrazyReport crazyReport)
    {
        _linuxCommand = linuxCommand;
        _coreMap = coreMap;
        _pluginConfiguration = pluginConfiguration;
        _logger = logger;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<LifecycleServices>(LifecycleModule.ModuleName);
        _jsonSerializerConfiguration = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
    }
    public async Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default)
    {
        string file = _pluginConfiguration.GetUserConfigFor(LifecycleModule.ModuleName, USER_DEF_STARTUP_PARAM_FILE);
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
            throw new WebServiceException($"Unable to load {USER_DEF_STARTUP_PARAM_FILE}");
        }
    }

    public async Task<GameInfoEntity?> LoadGameInfoAsync(CancellationToken cancellationToken = default)
    {
        string file = _pluginConfiguration.GetUserBashFor(LifecycleModule.ModuleName, [SERVER_CONTROL_FOLDER], GAMEINFO_FILE);
        _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<GameInfoResponse>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameInfoEntity>();
            return entity;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
            throw new WebServiceException($"Unable to load {GAMEINFO_FILE}");
        }
    }

    public Task ServerRestartAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task ServerStartAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public async Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetUserBashFor(LifecycleModule.ModuleName, [SERVER_CONTROL_FOLDER, SERVER_CONTROL_COMMON_FOLDER], SERVER_CONTROL_COMMON_STATUS_FILE);
        var result = await _linuxCommand.RunLinuxScriptWithReplyAs<StatusResponse>(script, (str) => new() { Completed = false, Failure = str });
        if (!result.Completed)
            throw new WebServiceException("Failed unable to get server status"); //TODO: Localize
        var resultEntity = _coreMap.Map(result).To<ServerInfoEntity>();
        return resultEntity;
    }

    public Task ServerStopAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public async Task UpdateStartupParameterAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        var script = _pluginConfiguration.GetBashFor(LifecycleModule.ModuleName, "update_startup_parameters.sh", key, value);
        var result = await _linuxCommand.RunLinuxScriptWithReplyAs<ScriptResponse>(script, (str) => new() { Completed = false, Failure = str });
        if (!result.Completed) throw new WebServiceException("Update of the startup parameter failed"); //TODO: Localize
    }
}
