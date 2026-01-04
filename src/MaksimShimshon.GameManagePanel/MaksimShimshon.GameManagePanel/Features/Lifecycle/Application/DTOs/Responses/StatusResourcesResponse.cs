namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusResourcesResponse
{
    public StatusCpuResponse? Cpu { get; set; }
    public StatusMemoryResponse? Memory { get; set; }
    public StatusStorageResponse? Storage { get; set; }
}