using MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components;

public partial class SystemResourceInfoGauges
{
    [Parameter] public InfoPanelFormFactor FormFactor { get; set; } = InfoPanelFormFactor.Normal;
    [Parameter] public float CurrentProcessor { get; set; }
    [Parameter] public float CurrentRam { get; set; }
    [Parameter] public float CurrentDisk { get; set; }




}
