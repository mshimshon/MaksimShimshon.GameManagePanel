using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Application.Services;

public interface IGameInfoService
{
    Task<string?> GetRawGameInfoAsync(CancellationToken ct = default);
    Task<GameInfoEntity?> LoadGameInfoAsync(CancellationToken cancellationToken = default);

}
