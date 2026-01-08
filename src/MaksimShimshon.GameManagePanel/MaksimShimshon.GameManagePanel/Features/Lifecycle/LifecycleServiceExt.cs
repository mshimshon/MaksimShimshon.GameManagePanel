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
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
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
        services.AddScoped<CommandRunner>();
        services.AddTransient<ILifecycleServices, LifecycleServices>();
        services.AddStatePulseAction<LifecycleFetchStartupParametersAction>();
        services.AddStatePulseAction<LifecycleFetchStartupParametersDoneAction>();
        services.AddStatePulseAction<LifecycleServerGameInfoUpdatedAction>();
        services.AddStatePulseAction<LifecycleServerStartAction>();
        services.AddStatePulseAction<LifecycleServerStartDoneAction>();
        services.AddStatePulseAction<LifecycleServerStatusTransitionDoneAction>();
        services.AddStatePulseAction<LifecycleServerStatusTransitionTickedAction>();
        services.AddStatePulseAction<LifecycleServerStatusUpdateDoneAction>();
        services.AddStatePulseAction<LifecycleServerStatusUpdateSkippedAction>();
        services.AddStatePulseAction<LifecycleServerStopAction>();
        services.AddStatePulseAction<LifecycleServerStopDoneAction>();
        services.AddStatePulseAction<LifecycleUpdateStartupParameterAction>();
        services.AddStatePulseAction<LifecycleUpdateStartupParameterDoneAction>();

        services.AddStatePulseEffect<LifecycleFetchStartupParametersEffect>();
        services.AddStatePulseEffect<LifecycleServerStartEffect>();
        services.AddStatePulseEffect<LifecycleServerStatusPeriodicUpdateEffect>();
        services.AddStatePulseEffect<LifecycleServerStatusTransitionEffect>();
        services.AddStatePulseEffect<LifecycleServerStopEffect>();
        services.AddStatePulseEffect<LifecycleUpdateStartupParameterEffect>();


        services.AddStatePulseReducer<LifecycleFetchStartupParametersDoneReducer>();
        services.AddStatePulseReducer<LifecycleServerGameInfoUpdatedReducer>();
        services.AddStatePulseReducer<LifecycleServerStatusTransitionDoneReducer>();
        services.AddStatePulseReducer<LifecycleServerStartDoneReducer>();
        services.AddStatePulseReducer<LifecycleServerStatusTransitionTickedReducer>();
        services.AddStatePulseReducer<LifecycleServerStatusUpdateDoneReducer>();
        services.AddStatePulseReducer<LifecycleServerStatusUpdateSkippedReducer>();
        services.AddStatePulseReducer<LifecycleServerStopDoneReducer>();

        services.AddStatePulseStateFeature<LifecycleGameInfoState>();
        services.AddStatePulseStateFeature<LifecycleServerState>();

        services.AddMedihaterRequestHandler<GetServerStatusQuery, GetServerStatusHandler, ServerInfoEntity?>();
        services.AddMedihaterRequestHandler<GetStartupParametersQuery, GetStartupParametersHandler, Dictionary<string, string>>();
        services.AddMedihaterRequestHandler<ExecRestartServerCommand, ExecRestartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStartServerCommand, ExecStartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStopServerCommand, ExecStopServerHandler>();
        services.AddMedihaterRequestHandler<ExecUpdateStartupParameterCommand, ExecUpdateStartupParameterHandler>();
    }
}
