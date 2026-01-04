using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses.Mapping;

public class StatusResponseToServerInfoEntity : ICoreMapHandler<StatusResponse, ServerInfoEntity>
{
    public ServerInfoEntity Handler(StatusResponse data, ICoreMap alsoMap)
    {
        Status currentStatus = Status.Unknown;
        if (string.Equals(data.Status, "started", StringComparison.InvariantCultureIgnoreCase))
            currentStatus = Status.Running;
        else if (string.Equals(data.Status, "stopped", StringComparison.InvariantCultureIgnoreCase))
            currentStatus = Status.Stopped;

        return new ServerInfoEntity()
        {
            Status = currentStatus,
            Port = data.Server?.Port.ToString() ?? "????",
            Name = data.Server?.Name ?? "Unknown",
            LastUpdate = data.Timestamp,
            GameInfo = alsoMap.Map(data.GameInfo).To<GameInfoEntity>(),
            SystemInfo = data.Resources != default && data.Resources.Memory != default && data.Resources.Cpu != default && data.Resources.Storage != default ? alsoMap.Map(data.Resources).To<SystemInfoEntity>() : default
        };
    }
}


