using Image_Utility.Commands;
using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Utility.ViewModels
{
    /// <summary>
    /// Created by the template
    /// make changes as needed
    /// </summary>
    public class AppViewModel : ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;
        public ICommand NavigateDownloaderCommand { get; }
        public ICommand NavigateResizerCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateCompresserCommand { get; }
        public ICommand NavigateRenamerCommand { get; }

        public AppViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
            NavigateDownloaderCommand = new NavigateCommand<DownloaderViewModel>(_navigator, () => new DownloaderViewModel(_navigator));
            NavigateResizerCommand = new NavigateCommand<ResizerViewModel>(_navigator, () => new ResizerViewModel(_navigator));
            NavigateSettingsCommand = new NavigateCommand<SettingsViewModel>(_navigator, () => new SettingsViewModel(_navigator));
            NavigateCompresserCommand = new NavigateCommand<CompresserViewModel>(_navigator, () => new CompresserViewModel(_navigator));
            NavigateRenamerCommand = new NavigateCommand<RenamerViewModel>(_navigator, () => new RenamerViewModel(_navigator));
        }

        private void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
