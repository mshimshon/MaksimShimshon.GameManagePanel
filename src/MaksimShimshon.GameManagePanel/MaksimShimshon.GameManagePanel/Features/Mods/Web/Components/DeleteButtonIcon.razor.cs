using Microsoft.AspNetCore.Components;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Web.Components;


public partial class DeleteButtonIcon<TEntity>
{

    private enum Stage
    {
        Initial,
        Confirm,
        Cancel
    }

    [Parameter]
    public EventCallback<TEntity> OnClick { get; set; }

    [Parameter]
    public int ResetDelay { get; set; } = 500;

    [Parameter]
    public int CancelDelay { get; set; } = 500;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public TEntity Item { get; set; } = default!;

    private Stage _stage = Stage.Initial;
    private CancellationTokenSource _waitCancelSource = new();
    private object _lockStage = new();
    private async Task InitialClick()
    {
        lock (_lockStage)
        {
            if (_stage != Stage.Initial) return;
            _waitCancelSource.Cancel();
            _stage = Stage.Confirm;
        }
        await InvokeAsync(StateHasChanged);
        _waitCancelSource = new();
        _ = Wait(ResetDelay, (ct) => _stage = Stage.Initial, _waitCancelSource.Token);
    }

    private async Task Confirm()
    {
        lock (_lockStage)
        {
            if (_stage != Stage.Confirm) return;
            _waitCancelSource.Cancel();
            _stage = Stage.Cancel;
        }
        await InvokeAsync(StateHasChanged);
        _waitCancelSource = new();
        _ = Wait(ResetDelay, (ct) => _stage = Stage.Initial, _waitCancelSource.Token);
    }

    private async Task Cancel()
    {
        lock (_lockStage)
        {
            if (_stage != Stage.Cancel) return;
            _waitCancelSource.Cancel();
            _stage = Stage.Initial;
        }
        await InvokeAsync(StateHasChanged);
        _waitCancelSource = new();
        _ = Wait(CancelDelay, (ct) =>
        {
            _stage = Stage.Initial;
            _ = OnClick.InvokeAsync(Item);
        }, _waitCancelSource.Token);
    }

    private async Task Wait(int delay, Action<CancellationToken> exec, CancellationToken ct = default)
    {
        await Task.Delay(delay, ct);
        lock (_lockStage)
        {
            if (ct.IsCancellationRequested) return;
            exec(ct);
        }
        if (ct.IsCancellationRequested) return;
        await InvokeAsync(StateHasChanged);
    }

}
