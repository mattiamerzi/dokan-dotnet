using System.Collections.Concurrent;
using DokanNet;
using DokanNet.Logging;
using DokanVsRemoteFS;

namespace WinVsRemoteClient;

internal class VsRemoteFSManager : IDisposable
{
    private readonly Dokan _dokan;
    private readonly ConcurrentDictionary<ConfigSite, Thread> _mountThreads;
    private readonly ConcurrentDictionary<ConfigSite, DokanInstance> _dokanInstances;
    private readonly ConcurrentDictionary<ConfigSite, DynaLogger> _dokanLoggers;
    private bool _disposed;

    public VsRemoteFSManager()
    {
        _dokan = new Dokan(new NullLogger());
        _mountThreads = new ConcurrentDictionary<ConfigSite, Thread>();
        _dokanInstances = new ConcurrentDictionary<ConfigSite, DokanInstance>();
        _dokanLoggers = new ConcurrentDictionary<ConfigSite, DynaLogger>();
    }

    public void MountVsRemoteFS(ConfigSite site)
    {
        ObjectDisposedException.ThrowIf(_disposed, typeof(VsRemoteFSManager));

        if (!_dokanLoggers.TryGetValue(site, out var dynaLogger))
            _dokanLoggers.TryAdd(site, dynaLogger = new());

        var mountThread = new Thread(() =>
        {
            DokanInstance? dokanInstance = null;
            try
            {
                var fs = new VsRemoteFS(site.Address, dynaLogger);
                var dokanBuilder = new DokanInstanceBuilder(_dokan)
                    .ConfigureLogger(() => dokanLogger)
                    .ConfigureOptions(options =>
                    {
                        options.Options = DokanOptions.RemovableDrive;
                        options.MountPoint = site.MountPoint;
                    });

                dokanInstance = dokanBuilder.Build(fs);

                if (_dokanInstances.TryAdd(site, dokanInstance))
                {
                    try
                    {
                        dokanInstance.WaitForFileSystemClosed(uint.MaxValue);
                    }
                    finally
                    {
                        _dokanInstances.TryRemove(site, out _);
                        dokanInstance.Dispose();
                    }
                }
                else
                {
                    dokanInstance.Dispose();
                    dynaLogger.Fatal($"Site {site.Label} is already mounted.");
                }
            }
            catch (Exception ex)
            {
                dynaLogger.Fatal($"Error mounting site {site.Label}: {ex.Message}");
                dokanInstance?.Dispose();
            }
        });

        if (_mountThreads.TryAdd(site, mountThread))
        {
            mountThread.Start();
        }
        else
        {
            dynaLogger.Fatal($"A mount operation is already running for site {site.Label}.");
        }
    }

    public void UnmountVsRemoteFS(ConfigSite site)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(VsRemoteFSManager));

        _dokanLoggers.TryGetValue(site, out var dynaLogger);

        if (_dokanInstances.TryRemove(site, out var dokanInstance))
        {
            try
            {
                _dokan.RemoveMountPoint(site.Label);
            }
            catch (Exception ex)
            {
                dynaLogger?.Fatal($"Error unmounting site {site.Label}: {ex.Message}");
            }
            finally
            {
                dokanInstance.Dispose();
            }
        }
        else
        {
            dynaLogger?.Fatal($"Site {site.Label} is not mounted.");
        }

        if (_mountThreads.TryRemove(site, out var mountThread))
        {
            try
            {
                mountThread.Join();
            }
            catch (Exception ex)
            {
                dynaLogger?.Fatal($"Error stopping thread for site {site.Label}: {ex.Message}");
            }
        }
    }

    public bool IsMounted(ConfigSite site)
    {
        // Verifica se il sito è nella lista di mount e se la thread associata è ancora in esecuzione
        return _mountThreads.ContainsKey(site) && _mountThreads[site].IsAlive;
    }

    public DynaLogger? GetLogger(ConfigSite site)
        => _dokanLoggers.TryGetValue(site, out var logger) ? logger : null;

    public void Dispose()
    {
        if (_disposed) return;

        foreach (var site in _dokanInstances.Keys)
        {
            UnmountVsRemoteFS(site);
        }
        _dokanLoggers.Clear();

        _dokan.Dispose();
        _disposed = true;
    }
}

