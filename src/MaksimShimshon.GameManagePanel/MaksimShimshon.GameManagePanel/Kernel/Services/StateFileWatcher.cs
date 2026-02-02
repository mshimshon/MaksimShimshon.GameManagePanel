using MaksimShimshon.GameManagePanel.Kernel.Services.Enums;
using StatePulse.Net;
using System.Collections.Concurrent;

namespace MaksimShimshon.GameManagePanel.Kernel.Services;

internal class StateFileWatcher<TAction> : IStateFileWatcher<TAction> where TAction : FileWatchActionBase
{
    private bool _disposed;
    private readonly FileSystemWatcher _fileSystemWatcher;
    private readonly IDispatcher _dispatcher;

    public string Directory { get; init; }
    public string FilePattern { get; init; }
    private readonly ConcurrentQueue<Func<Task>> _changeQueue = new();
    private readonly Task _queueWorker;

    public StateFileWatcher(string path, string filePattern, FileWatchEvents[] whatToWatch, IDispatcher dispatcher)
    {

        Directory = path;
        FilePattern = filePattern;
        _dispatcher = dispatcher;
        _fileSystemWatcher = new FileSystemWatcher(path, filePattern);
        _fileSystemWatcher.Created += DispatchNotify;
        _fileSystemWatcher.Changed += DispatchNotify;
        _fileSystemWatcher.Deleted += DispatchNotify;
        _fileSystemWatcher.Renamed += DispatchNotify;
        _fileSystemWatcher.EnableRaisingEvents = true;
        _queueWorker = WatchQueue();
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

        Enqueue(() => Notify(e, eventType));
    }
    private async Task Notify(FileSystemEventArgs args, FileWatchEvents eventType)
    {

        TAction action = Activator.CreateInstance<TAction>();
        if (action == default) return;
        action.Event = eventType;
        action.FullName = args.FullPath;
        action.Date = DateTime.UtcNow;
        action.FileName = args.Name!;
        await _dispatcher.Prepared(action).Await().DispatchAsync();
    }
    private readonly SemaphoreSlim _signal = new(0);

    private void Enqueue(Func<Task> work)
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
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    // TODO: Handle that shit
                    Console.WriteLine("WatchQueue exception: " + ex);
                }

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
            _queueWorker.Dispose();
        }

        // free unmanaged resources here

        _disposed = true;
    }


}
