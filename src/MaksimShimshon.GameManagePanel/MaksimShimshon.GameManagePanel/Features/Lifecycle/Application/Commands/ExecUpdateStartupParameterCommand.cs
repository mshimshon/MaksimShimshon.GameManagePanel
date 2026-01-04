using MedihatR;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Commands;

public record ExecUpdateStartupParameterCommand : IRequest
{
    public string Key { get; init; } = default!;
    public string Value { get; init; } = default!;
}
