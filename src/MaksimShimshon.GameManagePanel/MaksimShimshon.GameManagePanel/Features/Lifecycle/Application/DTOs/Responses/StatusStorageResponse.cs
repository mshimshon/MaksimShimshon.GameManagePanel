namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusStorageResponse
{
    public int Total { get; set; }
    public int Used { get; set; }
    public int Available { get; set; }
}

