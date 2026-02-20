using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Middlewares.StatePulse;

internal class DispatchErrorMiddleware : IDispatcherMiddleware
{
    private readonly ICrazyReport _crazyReport;

    public DispatchErrorMiddleware(ICrazyReport crazyReport)
    {
        _crazyReport = crazyReport;
        _crazyReport.SetModule<DispatchErrorMiddleware>(KernelModule.ModuleName);
    }
    public Task AfterDispatch(object action) => Task.CompletedTask;
    public Task BeforeDispatch(object action) => Task.CompletedTask;
    public Task OnDispatchFailure(Exception exception, object action)
    {
        _crazyReport.ReportError(exception.Message);
        return Task.CompletedTask;
    }
}
