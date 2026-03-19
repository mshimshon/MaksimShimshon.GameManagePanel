using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Hooks.Queries;

[QueryBusId(LifecycleKeys.Queries.GetRawGameInfo)]
internal class GetRawGameInfoQueryBus : IQueryBusHandler
{
    private readonly IMedihater _medihater;

    public GetRawGameInfoQueryBus(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
    {
        var query = new GetRawGameInfoQuery();
        var result = await _medihater.Send(query);
        return await qry.ReplyWith(result);
    }
}
