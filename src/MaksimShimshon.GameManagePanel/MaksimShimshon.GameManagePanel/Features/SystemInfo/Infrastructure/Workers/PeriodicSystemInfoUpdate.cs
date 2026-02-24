using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Configurations;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Workers;

internal sealed class PeriodicSystemInfoUpdate
{
    private readonly IDispatcher _dispatcher;
    private readonly LinuxSystemInfoConfiguration _linuxSystemInfoConfiguration;

    public PeriodicSystemInfoUpdate(IDispatcher dispatcher, LinuxSystemInfoConfiguration linuxSystemInfoConfiguration)
    {
        _dispatcher = dispatcher;
        _linuxSystemInfoConfiguration = linuxSystemInfoConfiguration;
    }
    public Task StartAsync(CancellationToken ct = default)
        => Task.Run(() => UpdateCycle(ct));

    private async Task UpdateCycle(CancellationToken ct = default)
    {
        do
        {
            await _dispatcher.Prepare<SystemInfoUpdateAction>().Await().DispatchAsync();

            await Task.Delay(_linuxSystemInfoConfiguration.PeriodicResourceCheckDelaySeconds * 1000);
        } while (!ct.IsCancellationRequested);
    }
}
/*
 * IScheduleRunner, IScheduleAction<TAction>, IScheduledStatePulseAction<TAction> TAction = IAction or class
 * ActionSchedule Record -> Datetime nextRun
 * IScheduledAction Task<ActionSchedule> Run(Ct); 
 * [ScheduledActionId("ID", InitialTick, RunOnceAtStartup)] =>  
 */