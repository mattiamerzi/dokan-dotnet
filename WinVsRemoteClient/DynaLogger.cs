using System.Collections.Concurrent;
using DokanNet.Logging;

namespace WinVsRemoteClient;

internal class DynaLogger : ILogger
{
    public bool DebugEnabled => false;
    public bool ErrorEnabled { get; set; } = false;
    public bool FatalEnabled { get; set; } = false;
    public bool InfoEnabled { get; set; } = false;
    public bool WarnEnabled { get; set; } = false;
    private readonly ConcurrentQueue<string> messages = new();
    public IEnumerable<string> Messages => messages;

    public void Debug(string message, params object[] args)
    {
    }

    private void Enqueue(string level, string message)
    {
        Console.WriteLine(message);
        if (messages.Count > 100)
            messages.TryDequeue(out var _);
        messages.Enqueue($"[{DateTime.Now:HH:mm:ss} [{level}] {message}");
    }

    public void Error(string message, params object[] args)
    {
        Enqueue("ERR", string.Format(message, args));
    }

    public void Fatal(string message, params object[] args)
    {
        Enqueue("!!!", string.Format(message, args));
    }

    public void Info(string message, params object[] args)
    {
        Enqueue("INF", string.Format(message, args));
    }

    public void Warn(string message, params object[] args)
    {
        Enqueue("WRN", string.Format(message, args));
    }

}
