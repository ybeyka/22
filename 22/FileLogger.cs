using System;
using System.IO;

public class FileLogger : ILogger
{
    private readonly string _logPath;

    public FileLogger(string logPath)
    {
        _logPath = logPath;
    }

    public void LogInfo(string message) => Log("[INFO]", message);
    public void LogWarning(string message) => Log("[WARNING]", message);
    public void LogError(string message) => Log("[ERROR]", message);

    private void Log(string logLevel, string message)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(_logPath))
            {
                sw.WriteLine($"{logLevel} {DateTime.Now} - {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not log the message: {ex.Message}");
        }
    }
}
