using LunaticPanel.Core.Abstraction.Tools;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Infrastructure.Services;

internal class GitService : IGitService
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ILinuxCommand _linuxCommand;

    public GitService(PluginConfiguration pluginConfiguration, ILinuxCommand linuxCommand)
    {
        _pluginConfiguration = pluginConfiguration;
        _linuxCommand = linuxCommand;
    }

    public async Task CloneAsync(string gitUrl, string target, CancellationToken ct = default)
    {

        var targetFolder = _pluginConfiguration.GetReposFor(LinuxGameServerModule.ModuleName, target);
        var command = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "gitclone.sh", gitUrl, targetFolder);
        Console.WriteLine($"Cloning {gitUrl} to {targetFolder}");
        var result = await _linuxCommand.RunLinuxScriptWithReplyAs<ScriptResponse>(command);
        if (!result.Completed)
            throw new WebServiceException(result.Failure);

        // Fix Permissions
        await _linuxCommand.RunLinuxCommand($"chmod 775 -R {targetFolder}");

    }

}
