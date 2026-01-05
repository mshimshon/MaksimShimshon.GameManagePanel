namespace MaksimShimshon.GameManagePanel.Kernel.Heartbeat;

internal interface IHeartbeatService
{
    Task StartBeatingAsync(CancellationToken ct = default);
}
