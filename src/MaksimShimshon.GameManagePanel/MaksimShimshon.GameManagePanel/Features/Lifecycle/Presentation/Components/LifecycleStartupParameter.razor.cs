using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Presentation.Components;

public partial class LifecycleStartupParameter
{
    private string GetInitialValue(GameStartupParameterEntity parameter) =>
        ViewModel.StartupParameters.ContainsKey(parameter.Key.Key) ?
        ViewModel.StartupParameters[parameter.Key.Key] :
        parameter.DefaultValue ?? string.Empty;
}
