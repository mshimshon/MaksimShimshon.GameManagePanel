using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Services;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
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

        var commandCheckGit = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "check_git_install.sh");
        var checkGitResult = await _linuxCommand
            .BuildBash(commandCheckGit)
            .Sudo()
            .ExecAndReadAs<ScriptResponse>((s) => new() { Failure = s }, ct);

        Console.WriteLine(checkGitResult.ToString());
        if (!checkGitResult.Completed) throw new WebServiceException(checkGitResult.Failure);
        var targetFolder = _pluginConfiguration.GetReposFor(LinuxGameServerModule.ModuleName, target);
        var command = _pluginConfiguration.GetBashFor(LinuxGameServerModule.ModuleName, "git_clone.sh", gitUrl, targetFolder);
        var result = await _linuxCommand.BuildBash(command)
            .Sudo()
            .ExecAndReadAs<ScriptResponse>((s) => new() { Failure = s }, ct);
        if (!result.Completed)
            throw new WebServiceException(result.Failure);

        // Fix Permissions
        await _linuxCommand.BuildCommand($"chmod 775 -R {targetFolder}").Sudo().ExecAsync(ct);

    }

}
