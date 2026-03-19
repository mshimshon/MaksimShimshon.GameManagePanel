using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Commands.Handlers;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MedihatR;
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

        services.AddStatePulseService<GetModListAction>();
        services.AddStatePulseService<GetModListDoneAction>();
        services.AddStatePulseService<GetModListEffect>();
        services.AddStatePulseService<GetModListDoneReducer>();
        services.AddStatePulseService<GetModListReducer>();

        services.AddStatePulseService<CreateModListAction>();
        services.AddStatePulseService<CreateModListDoneAction>();
        services.AddStatePulseService<CreateModListEffect>();
        services.AddStatePulseService<CreateModListReducer>();
        services.AddStatePulseService<CreateModListDoneReducer>();

        services.AddStatePulseService<GetAvailableModListAction>();
        services.AddStatePulseService<GetAvailableModListDoneAction>();
        services.AddStatePulseService<GetAvailableModListEffect>();
        services.AddStatePulseService<GetAvailableModListReducer>();
        services.AddStatePulseService<GetAvailableModListDoneReducer>();


        services.AddStatePulseService<LoadModListSchematicAction>();
        services.AddStatePulseService<LoadModListSchematicDoneAction>();
        services.AddStatePulseService<LoadModListSchematicEffect>();
        services.AddStatePulseService<LoadModListSchematicReducer>();
        services.AddStatePulseService<LoadModListSchematicDoneReducer>();


        services.AddStatePulseService<ModListState>();
        services.AddStatePulseService<ModListLocalState>();

        services.AddMedihaterRequestHandler<GetModListQuery, GetModListHandler, ModListEntity?>();
        services.AddMedihaterRequestHandler<CreateModListCommand, CreateModListHandler>();
        services.AddMedihaterRequestHandler<GetAllModListQuery, GetAllModListHandler, ICollection<ModListDescriptor>>();
        services.AddMedihaterRequestHandler<GetModSchematicQuery, GetModSchematicHandler, IReadOnlyCollection<PartSchematicEntity>?>();

    }
}
