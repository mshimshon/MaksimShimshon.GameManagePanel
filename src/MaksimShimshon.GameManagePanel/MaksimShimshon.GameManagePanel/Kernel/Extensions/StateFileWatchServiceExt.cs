using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Services;
using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace MaksimShimshon.GameManagePanel.Kernel.Extensions;

public static class StateFileWatchServiceExt
{
    public static void AddMasterStateFileWatcherService<TAction>(this IServiceCollection services, Func<PluginConfiguration, string> getBasePath, string filePattern, FileWatchEvents[] whatToWatch) where TAction : FileWatchActionBase
    {
        services.AddSingleton<IStateFileWatcher<TAction>>(sp => AddActionService<TAction>(sp, getBasePath.Invoke(sp.GetRequiredService<PluginConfiguration>()), filePattern, whatToWatch));
    }

    static StateFileWatcher<TAction> AddActionService<TAction>(IServiceProvider serviceProvider, string folder, string filePattern, FileWatchEvents[] whatToWatch)
        where TAction : FileWatchActionBase
        => new StateFileWatcher<TAction>(folder, filePattern, whatToWatch, serviceProvider.GetRequiredService<IDispatcher>());

    public static IStateFileWatcher<TAction> LoadWatcher<TAction>(this IServiceProvider sp)
        where TAction : FileWatchActionBase
        => sp.GetRequiredService<IStateFileWatcher<TAction>>();
}
