namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Hooks.Components.ViewModels;

public class WidgetSystemInfoViewModel : IWidgetSystemInfoViewModel
{
    private bool _loading = false;
    public bool IsLoading
    {
        get => _loading;
        private set
        {
            bool hasChanged = value != _loading;
            _loading = value;
            if (hasChanged)
                _ = OnUpdate();
        }
    }

    public event Func<Task>? SpreadChanges;



    private async Task OnUpdate() => _ = SpreadChanges?.Invoke();
    public WidgetSystemInfoViewModel()
    {
    }
}
