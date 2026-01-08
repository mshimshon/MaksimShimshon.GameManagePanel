using MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.CQRS.Queries.Handlers;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Actions;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Effects;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Pulses.Reducers;
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
        var windowsSysInfoConfig =
            configuration.GetSection("SystemInfo").GetSection("Windows")?.Get<WindowsSystemInfoConfiguration>() ??
            new WindowsSystemInfoConfiguration();

        var linuxSysInfoConfig =
        configuration.GetSection("SystemInfo").GetSection("Linux")?.Get<LinuxSystemInfoConfiguration>() ??
        new LinuxSystemInfoConfiguration();

        if (OperatingSystem.IsWindows())
            services.AddScoped<ISystemInfoService, WindowsSystemInfoService>();
        else if (OperatingSystem.IsLinux())
            services.AddScoped<ISystemInfoService, LinuxSystemInfoService>();

        services.AddScoped((sp) => windowsSysInfoConfig);
        services.AddScoped((sp) => linuxSysInfoConfig);

        services.AddScoped<ISystemResourcesStatusViewModel, SystemResourcesStatusViewModel>();
        services.AddScoped<IWidgetSystemInfoViewModel, WidgetSystemInfoViewModel>();
        services.AddScoped<CommandRunner>();
        services.AddStatePulseAction<SystemInfoUpdateAction>();
        services.AddStatePulseAction<SystemInfoUpdatedAction>();
        services.AddStatePulseEffect<SystemInfoUpdateEffect>();
        services.AddStatePulseReducer<ServerSystemInfoUpdatedReducer>();
        services.AddStatePulseStateFeature<SystemInfoState>();
        services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoEntity?>();
    }
}
