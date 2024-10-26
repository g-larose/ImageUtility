using Image_Utility.Commands;
using Image_Utility.Interfaces;
using Serilog;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Input;
using ILogger = Image_Utility.Interfaces.ILogger;

namespace Image_Utility.ViewModels
{
    /// <summary>
    /// Created by the template
    /// make changes as needed
    /// </summary>
    public class AppViewModel : ViewModelBase
    {
        private readonly INavigator? _navigator;
        private readonly ILogger _logger;
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;
        public ICommand NavigateDownloaderCommand { get; }
        public ICommand NavigateResizerCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateCompresserCommand { get; }
        public ICommand NavigateRenamerCommand { get; }

        private bool _isOnline;
        public bool IsOnline
        {
            get => _isOnline;
            set => OnPropertyChanged(ref _isOnline, value);
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get => _canExecute;
            set => OnPropertyChanged(ref _canExecute, value);
        }

        public AppViewModel(INavigator? navigator, ILogger logger)
        {
            _navigator = navigator;
            _logger = logger;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
            NavigateDownloaderCommand = new NavigateCommand<DownloaderViewModel>(_navigator, () => new DownloaderViewModel(_navigator));
            NavigateResizerCommand = new NavigateCommand<ResizerViewModel>(_navigator, () => new ResizerViewModel(_navigator, _logger, this));
            NavigateSettingsCommand = new NavigateCommand<SettingsViewModel>(_navigator, () => new SettingsViewModel(_navigator));
            NavigateCompresserCommand = new NavigateCommand<CompresserViewModel>(_navigator, () => new CompresserViewModel(_navigator));
            NavigateRenamerCommand = new NavigateCommand<RenamerViewModel>(_navigator, () => new RenamerViewModel(_navigator));
            CanExecute = true;
            IsOnline = NetworkInterface.GetIsNetworkAvailable();
            Log.Information("Image Utility Started");
        }

        private void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        public void IsConnectedToInternet()
        {
            string host = "google.com";  
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 8000);
                if (reply.Status == IPStatus.Success)
                    _isOnline = true;
            }
            catch (Exception e)
            {
                _isOnline = false;
            }
        }
    }
}
