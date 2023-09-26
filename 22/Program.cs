using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void CreateTestLogFile(string path)
    {
        string[] logEntries = {
            "[INFO] 2023-09-26 08:00:00 - Application started",
            "[WARNING] 2023-09-26 08:05:00 - Low memory",
            "[INFO] 2023-09-26 08:10:00 - User logged in",
            "[ERROR] 2023-09-26 08:15:00 - Connection lost",
            "[INFO] 2023-09-26 08:20:00 - User logged out",
            "[INFO] 2023-09-26 08:25:00 - Application finished"
        };

        try
        {
            File.WriteAllLines(path, logEntries);
            Console.WriteLine("Log file has been created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not create the log file: {ex.Message}");
        }
    }

    static void Main()
    {
        ILogger logger = new FileLogger("E:\\22\\log_report.txt");
        string logFilePath = "E:\\22\\your_log_file.txt";

        CreateTestLogFile(logFilePath);

        logger.LogInfo("Application started");

        try
        {
            var logFileLines = File.ReadLines(logFilePath);
            AnalyzeLogFile(logFileLines, logger);
        }
        catch (FileNotFoundException)
        {
            logger.LogError($"Log file at path {logFilePath} does not exist");
        }
        catch (Exception ex)
        {
            logger.LogError($"An unexpected error occurred: {ex.Message}");
        }

        logger.LogInfo("Application finished");
    }

    static void AnalyzeLogFile(IEnumerable<string> logFileLines, ILogger logger)
    {
        int numberOfErrors = logFileLines.Count(line => line.Contains("[ERROR]"));
        int numberOfWarnings = logFileLines.Count(line => line.Contains("[WARNING]"));
        int numberOfInfos = logFileLines.Count(line => line.Contains("[INFO]"));

        var report = new List<string>
        {
            "Log File Analysis Report",
            $"Number of Error messages: {numberOfErrors}",
            $"Number of Warning messages: {numberOfWarnings}",
            $"Number of Info messages: {numberOfInfos}"
        };

        string reportPath = "E:\\22\\zvit.txt";
        File.WriteAllLines(reportPath, report);

        logger.LogInfo("Report has been created successfully");
    }
}
