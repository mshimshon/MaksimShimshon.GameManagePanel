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
    private readonly ConcurrentQueue<StateFileNotifyQueueElement> _changeQueue = new();
    private readonly Task _queueWorker;
    private readonly CancellationTokenSource _cts = new();

    private readonly ConcurrentDictionary<string, long> _pendingByPath = new();
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
        _queueWorker = Task.Run(() => WatchQueue(_cts.Token));
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
        var version = _pendingByPath.AddOrUpdate(
            e.FullPath,
            _ => 1,
            (_, v) => v + 1
        );

        var action = new StateFileNotifyQueueElement()
        {
            Version = version,
            Path = e.FullPath,
            Action = () => Notify(e, eventType)
        };
        Enqueue(action);
    }
    private async Task Notify(FileSystemEventArgs args, FileWatchEvents eventType)
    {

        TAction action = Activator.CreateInstance<TAction>();
        if (action == default) return;
        action.Event = eventType;
        action.FullName = args.FullPath;
        action.Date = DateTime.UtcNow;
        action.FileName = args.Name!;
        _ = Task.Run(async () =>
        {
            try
            {
                await _dispatcher.Prepared(action).DispatchAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dispatch error: " + ex);
            }
        });

    }
    private readonly SemaphoreSlim _signal = new(0);

    private void Enqueue(StateFileNotifyQueueElement work)
    {
        _changeQueue.Enqueue(work);
        _signal.Release();
    }

    private async Task WatchQueue(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await _signal.WaitAsync(ct).ConfigureAwait(false);

            if (_changeQueue.TryDequeue(out var next))
                try
                {
                    // only keep lastest update and kill any stale
                    var currentVersion = _pendingByPath[next.Path];
                    if (currentVersion > next.Version) continue;

                    await next.Action().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // TODO: Handle that shit
                    Console.WriteLine("WatchQueue exception: " + ex);
                }
                finally
                {
                    if (_pendingByPath.TryGetValue(next.Path, out var currentVersion) && currentVersion == next.Version)
                    {
                        _pendingByPath.TryRemove(next.Path, out _);
                    }
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
            _cts.Cancel();
            _fileSystemWatcher.Dispose();
        }

        // free unmanaged resources here

        _disposed = true;
    }


}
