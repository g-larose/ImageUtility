using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class SettingsViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ViewModelBase? SelectedViewModel => _navigator.CurrentViewModel;

        public SettingsViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnSelectedViewModelChanged;
        }

        private void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
