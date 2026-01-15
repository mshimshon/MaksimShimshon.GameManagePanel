using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands.Handlers;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.UI.Components.ViewModels;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle;

public static class LifecycleServiceExt
{
    public static void AddLifecycleFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<IWidgetServerControlViewModel, WidgetServerControlViewModel>();
        services.AddScoped<IWidgetStartupParametersViewModel, WidgetStartupParametersViewModel>();
        services.AddScoped<ILifecycleStartupParameterViewModel, LifecycleStartupParameterViewModel>();

        services.AddTransient<ILifecycleStartupParameterFieldViewModel, LifecycleStartupParameterFieldViewModel>();
        services.AddTransient<ILifecycleServices, LifecycleServices>();
        services.AddStatePulseService<LifecycleFetchStartupParametersAction>();
        services.AddStatePulseService<LifecycleFetchStartupParametersDoneAction>();
        services.AddStatePulseService<LifecycleServerGameInfoUpdatedAction>();
        services.AddStatePulseService<LifecycleServerStartAction>();
        services.AddStatePulseService<LifecycleServerStartDoneAction>();
        services.AddStatePulseService<LifecycleServerStatusTransitionDoneAction>();
        services.AddStatePulseService<LifecycleServerStatusTransitionTickedAction>();
        services.AddStatePulseService<LifecycleServerStatusUpdateDoneAction>();
        services.AddStatePulseService<LifecycleServerStatusUpdateSkippedAction>();
        services.AddStatePulseService<LifecycleServerStopAction>();
        services.AddStatePulseService<LifecycleServerStopDoneAction>();
        services.AddStatePulseService<LifecycleUpdateStartupParameterAction>();
        services.AddStatePulseService<LifecycleUpdateStartupParameterDoneAction>();

        services.AddStatePulseService<LifecycleFetchStartupParametersEffect>();
        services.AddStatePulseService<LifecycleServerStartEffect>();
        services.AddStatePulseService<LifecycleServerStatusPeriodicUpdateEffect>();
        services.AddStatePulseService<LifecycleServerStatusTransitionEffect>();
        services.AddStatePulseService<LifecycleServerStopEffect>();
        services.AddStatePulseService<LifecycleUpdateStartupParameterEffect>();


        services.AddStatePulseService<LifecycleFetchStartupParametersDoneReducer>();
        services.AddStatePulseService<LifecycleServerGameInfoUpdatedReducer>();
        services.AddStatePulseService<LifecycleServerStatusTransitionDoneReducer>();
        services.AddStatePulseService<LifecycleServerStartDoneReducer>();
        services.AddStatePulseService<LifecycleServerStatusTransitionTickedReducer>();
        services.AddStatePulseService<LifecycleServerStatusUpdateDoneReducer>();
        services.AddStatePulseService<LifecycleServerStatusUpdateSkippedReducer>();
        services.AddStatePulseService<LifecycleServerStopDoneReducer>();

        services.AddStatePulseService<LifecycleGameInfoState>();
        services.AddStatePulseService<LifecycleServerState>();

        services.AddMedihaterRequestHandler<GetServerStatusQuery, GetServerStatusHandler, ServerInfoEntity?>();
        services.AddMedihaterRequestHandler<GetStartupParametersQuery, GetStartupParametersHandler, Dictionary<string, string>>();
        services.AddMedihaterRequestHandler<ExecRestartServerCommand, ExecRestartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStartServerCommand, ExecStartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStopServerCommand, ExecStopServerHandler>();
        services.AddMedihaterRequestHandler<ExecUpdateStartupParameterCommand, ExecUpdateStartupParameterHandler>();
    }
}
