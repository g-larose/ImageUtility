using Image_Utility.Interfaces;
using Image_Utility.Navigation;
using Image_Utility.Services;
using Image_Utility.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Windows;
using ILogger = Image_Utility.Interfaces.ILogger;

namespace Image_Utility
{
    /// <summary>
    /// This is created by the template
    /// make changes as needed
    /// Author: Async(void)
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
           
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<AppViewModel>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<ILogger, LoggerService>();
                services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<AppViewModel>()
                });
                
            }).Build();

            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.Debug(outputTemplate: "[{Timestamp:yyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{Newline}{Exception}")
                 .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Logs", "log.txt"))
                 .CreateLogger();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
