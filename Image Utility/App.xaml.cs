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
using System.Threading.Tasks;
using System.Windows;
using Image_Utility.Enums;
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
                services.AddSingleton<IResizer, ResizerService>();
                services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<AppViewModel>()
                });
                
            }).Build();
            
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            try
            {
                await _host.StopAsync();
                base.OnExit(e);
            }
            catch (Exception exception)
            {
                ILogger logger = new LoggerService();
                logger.Log(DateTime.UtcNow, exception.Message, LogType.Error, TargetType.File);
            }
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
