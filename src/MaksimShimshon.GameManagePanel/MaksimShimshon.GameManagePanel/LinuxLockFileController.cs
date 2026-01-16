using System.Collections.Concurrent;

namespace MaksimShimshon.GameManagePanel;

internal sealed class LinuxLockFileController : ILinuxLockFileController, IDisposable
{

    public ConcurrentDictionary<Guid, string> LockFiles { get; init; } = new();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private bool _disposed;
    private void Dispose(bool disposing)
    {
        if (_disposed || !disposing) return;
        _disposed = true;
        foreach (var path in LockFiles.Select(p => p.Value))
            if (File.Exists(path))
                try { File.Delete(path); }
                catch
                {
                    // clean up, only
                }
    }


    public Task ReleaseLockAsync(Guid id)
    {
        bool hasRemoved = LockFiles.TryRemove(id, out string? path);
        if (!hasRemoved || path == default) return Task.CompletedTask;
        Guid storedId = Guid.Parse(File.ReadAllText(path));
        if (storedId != id) return Task.CompletedTask;
        File.Delete(path);
        return Task.CompletedTask;
    }


    public async Task<Guid> TryToLockAsync(string path)
    {
        try
        {
            Guid id = Guid.NewGuid();
            using var fs = new FileStream(
                path,
                FileMode.CreateNew,   // <‑‑ atomic
                FileAccess.Write,
                FileShare.None);
            fs.Dispose();
            await File.WriteAllTextAsync(path, id.ToString()); // optional
            LockFiles.TryAdd(id, path);
            return id; // lock acquired
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Guid.Empty;
        }
    }

}
