using CoreMap;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Enums;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;

public class StatusResponseToServerInfoEntity : ICoreMapHandler<StatusResponse, ServerInfoEntity>
{
    public ServerInfoEntity Handler(StatusResponse data, ICoreMap alsoMap)
    {
        if (data.Data == default)
            return new ServerInfoEntity(Status.Unknown);

        Status currentStatus = Status.Unknown;
        if (data.Data.Status == 1)
            currentStatus = Status.Running;
        else if (data.Data.Status == 0)
            currentStatus = Status.Stopped;


        return new ServerInfoEntity(currentStatus)
        {
            Status = currentStatus,
            Port = data.Data.Port?.ToString() ?? "????",
            Name = data.Data.Name ?? "Unknown",
            LastUpdate = DateTime.UtcNow,
        };
    }
}


