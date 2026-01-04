using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;
using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Queries;

public record GetServerStatusQuery : IRequest<ServerInfoEntity>;
