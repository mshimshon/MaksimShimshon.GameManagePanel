using CoreMap;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Models;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Dto.Mapping;

internal class InstallationProgressToInstallationProcessModel : ICoreMapHandler<InstallationProgressStateDto, GameServerInstallProcessModel>
{
    public GameServerInstallProcessModel Handler(InstallationProgressStateDto data, ICoreMap alsoMap)
        => new GameServerInstallProcessModel()
        {
            CurrentStep = data.CurrentStep,
            DisplayName = data.DisplayName,
            Failed = data.Failed,
            Id = data.Id,
            IsInstalling = data.IsInstalling
        };
}
