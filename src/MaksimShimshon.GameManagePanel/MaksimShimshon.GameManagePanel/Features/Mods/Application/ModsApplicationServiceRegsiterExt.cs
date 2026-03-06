using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Application;

public static class ModsApplicationServiceRegsiterExt
{
    public static void RegisterModApplicationServices(this IServiceCollection services)
    {
        services.AddStatePulseService<UpdateCurrentModListAction>();
        services.AddStatePulseService<UpdateCurrentModListDoneAction>();
        services.AddStatePulseService<UpdateCurrentModListEffect>();
        services.AddStatePulseService<UpdateCurrentModListDoneReducer>();
        services.AddStatePulseService<UpdateCurrentModlistReducer>();

        services.AddStatePulseService<ModListState>();

    }
}
