namespace MaksimShimshon.GameManagePanel;

public interface ILinuxLockFileController
{
    Task<Guid> TryToLockAsync(string path);
    Task ReleaseLockAsync(Guid id);
}
