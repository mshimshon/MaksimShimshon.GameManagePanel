using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers.Middlewares;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.States;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Configurations;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Components.ViewModels;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Web.Hooks.Components.ViewModels;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo;

public static class SystemInfoServiceExt
{
    public static void AddSystemInfoFeatureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var linuxSysInfoConfig =
        configuration.GetSection("SystemInfo").GetSection("Linux")?.Get<LinuxSystemInfoConfiguration>() ??
        new LinuxSystemInfoConfiguration();

        services.AddScoped<ISystemInfoService, LinuxSystemInfoService>();

        services.AddScoped((sp) => linuxSysInfoConfig);

        services.AddScoped<ISystemResourcesStatusViewModel, SystemResourcesStatusViewModel>();
        services.AddScoped<IWidgetSystemInfoViewModel, WidgetSystemInfoViewModel>();
        services.AddStatePulseService<SystemInfoUpdateAction>();
        services.AddStatePulseService<SystemInfoUpdatedAction>();
        services.AddStatePulseService<SystemInfoUpdateEffect>();
        services.AddStatePulseService<ServerSystemInfoUpdatedReducer>();
        services.AddStatePulseService<SystemInfoState>();
        services.AddStatePulseService<OnServerInfoUpdateMiddleware>();
        services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoEntity?>();
    }
}
