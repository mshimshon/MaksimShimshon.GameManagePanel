using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Queries;

public record GetStartupParametersQuery : IRequest<Dictionary<string, string>>;
