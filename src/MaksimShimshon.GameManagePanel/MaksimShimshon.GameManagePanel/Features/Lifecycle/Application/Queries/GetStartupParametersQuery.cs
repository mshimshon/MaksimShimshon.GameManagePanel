using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Queries;

public record GetStartupParametersQuery : IRequest<Dictionary<string, string>>;
