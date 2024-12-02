using System.Collections.Concurrent;
using DokanNet.Logging;

namespace WinVsRemoteClient;

internal class DynaLogger : ILogger
{
    public bool DebugEnabled { get; set; } = true;
    public bool ErrorEnabled { get; set; } = false;
    public bool FatalEnabled { get; set; } = false;
    public bool InfoEnabled { get; set; } = false;
    public bool WarnEnabled { get; set; } = false;
    private readonly ConcurrentQueue<LogLine> messages = new();
    public IEnumerable<LogLine> GetMessages()
    {
        if (!messages.IsEmpty)
        {
            List<LogLine> newmsgs = new();
            while (messages.TryDequeue(out var s))
                newmsgs.Add(s);
            return newmsgs;
        }
        else
            return [];
    }

    private void Enqueue(LogLevel level, string message)
    {
        if (messages.Count > 100)
            messages.TryDequeue(out var _);
        messages.Enqueue(new(level, $"[{DateTime.Now:HH:mm:ss} [{DecodeLevel()}] {message}"));

        string DecodeLevel()
            => level switch
            {
                LogLevel.DEBUG => "DBG",
                LogLevel.ERROR => "ERR",
                LogLevel.FATAL => "!!!",
                LogLevel.WARN => "WRN",
                LogLevel.INFO => "INF",
                _ => string.Empty
            };
    }

    public void Debug(string message, params object[] args)
        => Enqueue(LogLevel.DEBUG, message);

    public void Error(string message, params object[] args)
        => Enqueue(LogLevel.ERROR, string.Format(message, args));

    public void Fatal(string message, params object[] args)
        => Enqueue(LogLevel.FATAL, string.Format(message, args));

    public void Info(string message, params object[] args)
        => Enqueue(LogLevel.INFO, string.Format(message, args));

    public void Warn(string message, params object[] args)
        => Enqueue(LogLevel.WARN, string.Format(message, args));

}
internal enum LogLevel
{
    ERROR, FATAL, INFO, WARN, DEBUG
}
internal record struct LogLine(LogLevel LogLevel, string Log);
