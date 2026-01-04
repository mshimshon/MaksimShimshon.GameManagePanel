using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.CQRS.Commands;

public record ExecUpdateStartupParameterCommand : IRequest
{
    public string Key { get; init; } = default!;
    public string Value { get; init; } = default!;
}
