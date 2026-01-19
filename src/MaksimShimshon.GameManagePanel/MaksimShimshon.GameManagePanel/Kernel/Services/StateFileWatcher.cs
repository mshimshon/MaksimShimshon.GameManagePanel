using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using StatePulse.Net;
using System.Collections.Concurrent;

namespace MaksimShimshon.GameManagePanel.Kernel.Services;

internal class StateFileWatcher<TAction> : IStateFileWatcher<TAction> where TAction : FileWatchActionBase
{
    private bool _disposed;
    private FileSystemWatcher _fileSystemWatcher;
    private readonly IDispatcher _dispatcher;

    public string Directory { get; init; }
    public string FilePattern { get; init; }
    private readonly ConcurrentQueue<Func<Task>> _changeQueue = new();
    public StateFileWatcher(string path, string filePattern, FileWatchEvents[] whatToWatch, IDispatcher dispatcher)
    {

        Directory = path;
        FilePattern = filePattern;
        _dispatcher = dispatcher;
        _fileSystemWatcher = new FileSystemWatcher(path, filePattern);
        _fileSystemWatcher.Created += DispatchNotify;
        _ = WatchQueue();
    }


    private void DispatchNotify(object _, FileSystemEventArgs e)
    {
        if (e.ChangeType == WatcherChangeTypes.Renamed) return;
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        var eventType = e.ChangeType switch
        {
            WatcherChangeTypes.Created => FileWatchEvents.Created,
            WatcherChangeTypes.Deleted => FileWatchEvents.Removed,
            WatcherChangeTypes.Changed => FileWatchEvents.Updated
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        _changeQueue.Enqueue(() => Notify(e, eventType));
    }
    private async Task Notify(FileSystemEventArgs args, FileWatchEvents eventType)
    {


        var action = Activator.CreateInstance<TAction>() as FileWatchActionBase;
        if (action == default) return;
        action.Event = eventType;
        action.FullName = args.FullPath;
        action.Date = DateTime.UtcNow;
        action.FileName = args.Name!;
        await _dispatcher.Prepared(action).Await().DispatchAsync();
    }
    private readonly SemaphoreSlim _signal = new(0);

    public void Enqueue(Func<Task> work)
    {
        _changeQueue.Enqueue(work);
        _signal.Release();
    }

    private async Task WatchQueue(CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            await _signal.WaitAsync(ct);

            if (_changeQueue.TryDequeue(out var next))
                await next();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _fileSystemWatcher.Dispose();
        }

        // free unmanaged resources here

        _disposed = true;
    }


}
