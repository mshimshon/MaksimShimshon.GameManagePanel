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
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Dto.Mapping;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Kernel.Extensions;
using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle;

public static class LifecycleServiceExt
{
    private const string USER_DEF_STARTUP_PARAM_FILE = "user_defined_startup_params.json";

    public static void AddLifecycleFeatureServices(this IServiceCollection services, bool isMaster)
    {
        services.AddScoped<IServerControlViewModel, ServerControlViewModel>();
        services.AddScoped<IStartupParameterViewModel, StartupParameterViewModel>();

        services.AddTransient<IStartupParameterFieldViewModel, StartupParameterFieldViewModel>();
        services.AddTransient<ILifecycleServices, LifecycleServices>();
        services.AddStatePulseService<FetchStartupParametersAction>();
        services.AddStatePulseService<FetchStartupParametersDoneAction>();
        services.AddStatePulseService<ServerGameInfoUpdateDoneAction>();
        services.AddStatePulseService<ServerStartAction>();
        services.AddStatePulseService<ServerStartDoneAction>();
        services.AddStatePulseService<ServerStatusTransitionDoneAction>();
        services.AddStatePulseService<ServerStatusUpdateDoneAction>();
        services.AddStatePulseService<ServerStopAction>();
        services.AddStatePulseService<ServerStopDoneAction>();
        services.AddStatePulseService<UpdateStartupParameterAction>();
        services.AddStatePulseService<UpdateStartupParameterDoneAction>();
        services.AddStatePulseService<ServerGameInfoUpdateAction>();
        services.AddStatePulseService<ServerGameInfoUpdateEffect>();
        services.AddStatePulseService<FetchStartupParametersEffect>();
        services.AddStatePulseService<ServerStartEffect>();
        services.AddStatePulseService<ServerStopEffect>();
        services.AddStatePulseService<UpdateStartupParameterEffect>();
        services.AddStatePulseService<FetchStartupParametersDoneReducer>();
        services.AddStatePulseService<ServerGameInfoUpdatedReducer>();
        services.AddStatePulseService<ServerStatusTransitionDoneReducer>();
        services.AddStatePulseService<ServerStartDoneReducer>();
        services.AddStatePulseService<ServerStatusUpdateDoneReducer>();
        services.AddStatePulseService<ServerStopDoneReducer>();
        services.AddStatePulseService<GameInfoState>();
        services.AddStatePulseService<ServerState>();

        services.AddStatePulseService<ServerStatusUpdateAction>();
        services.AddStatePulseService<ServerStatusUpdateEffect>();

        services.AddMedihaterRequestHandler<GetServerStatusQuery, GetServerStatusHandler, ServerInfoEntity?>();
        services.AddMedihaterRequestHandler<GetStartupParametersQuery, GetStartupParametersHandler, Dictionary<string, string>>();
        services.AddMedihaterRequestHandler<ExecRestartServerCommand, ExecRestartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStartServerCommand, ExecStartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStopServerCommand, ExecStopServerHandler>();
        services.AddMedihaterRequestHandler<ExecUpdateStartupParameterCommand, ExecUpdateStartupParameterHandler>();
        services.AddMedihaterRequestHandler<GetGameInfoQuery, GetGameInfoHandler, GameInfoEntity?>();

        services.AddCoreMapHandler<AllowedValueResponseToAllowedValueEntity>();
        services.AddCoreMapHandler<GameStartupParameterResponseToStartupParameter>();
        services.AddCoreMapHandler<GameStartupParamRespToGameStartupParamEntity>();
        services.AddCoreMapHandler<RelatedToResponseToConstraintTypeEntity>();
        services.AddCoreMapHandler<ValidationResponseToValidationEntity>();
        services.AddCoreMapHandler<GameInfoResponseToGameInfoEntity>();
        services.AddCoreMapHandler<StatusResponseToServerInfoEntity>();

        if (isMaster)
        {
            services.AddMasterStateFileWatcherService<FetchStartupParametersAction>(
                c =>
                {
                    string path = c.GetUserConfigBase(LifecycleModule.ModuleName);
                    Console.WriteLine(path);
                    return path;
                },
                USER_DEF_STARTUP_PARAM_FILE,
                [FileWatchEvents.Any]);

        }
    }
    public static async Task RuntimeLifecycleInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        serviceProvider.LoadWatcher<FetchStartupParametersAction>();
    }

}
