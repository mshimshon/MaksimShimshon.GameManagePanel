using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Presentation.Hooks.Components.ViewModels;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo;

public static class SystemInfoServiceExt
{
    public static void AddSystemInfoFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<ISystemResourcesStatusViewModel, SystemResourcesStatusViewModel>();
        services.AddScoped<IWidgetSystemInfoViewModel, WidgetSystemInfoViewModel>();
        services.AddScoped<CommandRunner>();
        services.AddTransient<ISystemInfoService, SystemInfoService>();
        services.AddStatePulseAction<SystemInfoUpdatedAction>();
        services.AddStatePulseEffect<SystemInfoUpdateEffect>();
        services.AddStatePulseReducer<ServerSystemInfoUpdatedReducer>();
        services.AddStatePulseStateFeature<SystemInfoState>();
        services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoEntity?>();
    }
}
