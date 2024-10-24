using Image_Utility.Interfaces;
using Image_Utility.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Image_Utility.Services
{
    public class LoggerService : ILogger
    {
        public void Log(DateTime timeStamp, string message)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs", "log.txt");
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs", "logs.json");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs"));

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"[{timeStamp:g}]: {message}");
            }
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize<SystemLog>(
                new SystemLog()
                {
                    Id = Guid.NewGuid(),
                    Content = message,
                    Author = "AppViewModel",
                    Timestamp = timeStamp,
                    LogLevel = SystemLogLevel.INFORMATION
                });

            File.AppendAllText(jsonPath, json);
        }
    }
}
