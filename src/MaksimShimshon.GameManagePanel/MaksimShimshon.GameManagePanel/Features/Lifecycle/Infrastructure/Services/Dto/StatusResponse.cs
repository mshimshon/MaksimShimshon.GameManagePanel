using MaksimShimshon.GameManagePanel.Kernel.Dto;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto;

public sealed record StatusResponse : ScriptResponse
{
    public StatusServerResponse? Data { get; set; }
}