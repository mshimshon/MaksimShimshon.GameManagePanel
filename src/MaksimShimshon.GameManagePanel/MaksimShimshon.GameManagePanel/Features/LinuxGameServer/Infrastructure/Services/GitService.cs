using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class GitService : IGitService
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly CommandRunner _commandRunner;

    public GitService(PluginConfiguration pluginConfiguration, CommandRunner commandRunner)
    {
        _pluginConfiguration = pluginConfiguration;
        _commandRunner = commandRunner;
    }

    public async Task CloneAsync(string gitUrl, string target, CancellationToken ct = default)
    {

        var targetFolder = _pluginConfiguration.ReposFolder;
        var cloneLocation = Path.Combine(targetFolder, target);
        var command = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "gitclone.sh", gitUrl, cloneLocation);

        var result = await _commandRunner.RunLinuxScriptWithReplyAs<ScriptResponse>(command);
        if (!result.Completed)
            throw new WebServiceException(result.Failure);
    }

}
