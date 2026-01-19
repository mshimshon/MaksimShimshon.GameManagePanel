using CoreMap;
using Microsoft.Extensions.DependencyInjection;

namespace MaksimShimshon.GameManagePanel.Kernel.Extensions;

public static class CoreMapExt
{
    public static void AddCoreMapHandler<TCoreMapHandler>(this IServiceCollection services)
        where TCoreMapHandler : class
    {
        var handlerType = typeof(TCoreMapHandler);

        var iface = handlerType
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICoreMapHandler<,>));

        services.AddTransient(iface, handlerType);
    }
}
