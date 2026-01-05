namespace MaksimShimshon.GameManagePanel.Kernel.Configuration;

public class GameInfoConfiguration
{
    public string Name { get; init; } = default!;
    public string LifeCycleProvider { get; set; } = default!;
    public string? SteamGameAppId { get; set; }
    public string? SteamServerAppId { get; set; }
    public bool SteamUseWorkshop { get; set; }
    public bool CanBeModded { get; set; }
    public bool RequiredManualModUpload { get; set; }
}
