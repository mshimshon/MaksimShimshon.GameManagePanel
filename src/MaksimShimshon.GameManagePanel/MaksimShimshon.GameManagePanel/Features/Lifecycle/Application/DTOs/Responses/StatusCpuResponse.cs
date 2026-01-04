namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.DTOs.Responses;

public record StatusCpuResponse
{
    public string Model { get; set; } = default!;
    public int Cores { get; set; }
    public int Usage { get; set; }
}


