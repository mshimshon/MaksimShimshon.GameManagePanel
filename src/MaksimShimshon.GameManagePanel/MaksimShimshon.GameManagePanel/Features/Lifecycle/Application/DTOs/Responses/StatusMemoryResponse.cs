namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusMemoryResponse
{
    public int Total { get; set; }
    public int Used { get; set; }
    public int Free { get; set; }
}
