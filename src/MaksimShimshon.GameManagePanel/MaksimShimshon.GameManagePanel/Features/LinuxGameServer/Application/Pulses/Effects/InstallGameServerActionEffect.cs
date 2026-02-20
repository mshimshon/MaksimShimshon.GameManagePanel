using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;
using MedihatR;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.LinuxGameServer.Application.Pulses.Effects;

public record InstallGameServerActionEffect : IEffect<InstallGameServerAction>
{
    private readonly IMedihater _medihater;
    private readonly ICrazyReport _crazyReport;

    public InstallGameServerActionEffect(IMedihater medihater, ICrazyReport crazyReport)
    {
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<InstallGameServerActionEffect>(LinuxGameServerModule.ModuleName);
    }
    public async Task EffectAsync(InstallGameServerAction action, IDispatcher dispatcher)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await _medihater.Send(new InstallGameServerCommand(action.Id, action.DisplayName));
            }
            catch (Exception ex)
            {
                _crazyReport.ReportError(ex.Message);
                await dispatcher.Prepare<GameServerInstallFailedAction>()
                    .With(p => p.Id, action.Id)
                    .With(p => p.DisplayName, action.DisplayName)
                    .With(p => p.FailureReason, "Cannot trigger installation script for some reasons") //TODO: Localize
                    .DispatchAsync();
            }
        });

    }
}
