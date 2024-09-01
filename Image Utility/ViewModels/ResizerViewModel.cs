using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class ResizerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;

        public ResizerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
        }

        private void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
