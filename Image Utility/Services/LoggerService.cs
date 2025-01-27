using Image_Utility.Interfaces;
using Image_Utility.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Image_Utility.Enums;

namespace Image_Utility.Services
{
    public class LoggerService : ILogger
    {

        public void Log(DateTime timeStamp, string message, LogType type, TargetType target)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs", "log.txt");
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs", "logs.json");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs"));

            switch (target)
            {
                case TargetType.Console:
                    Console.WriteLine($"[{timeStamp}]: [{type.ToString()}] {message}");
                    break;
                case TargetType.Debug:
                    Debug.WriteLine($"[{timeStamp}]: [{type.ToString()}] {message}");
                    break;
                case TargetType.File:
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine($"[{timeStamp:g}]: [{type.ToString()}] {message}");
                    }
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                    };

                    var json = JsonSerializer.Serialize<SystemLog>(
                        new SystemLog()
                        {
                            Id = Guid.NewGuid(),
                            Content = message,
                            Author = "AppViewModel",
                            Timestamp = timeStamp,
                            LogType = LogType.Information
                        }, options);

                    File.AppendAllText(jsonPath, json);
                    break;
            }
           
        }
    }
}
