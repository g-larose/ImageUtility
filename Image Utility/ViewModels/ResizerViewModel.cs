using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class ResizerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;

        private ObservableCollection<ImageProperties> _previews;
        public ObservableCollection<ImageProperties> Previews
        {
            get => _previews;
            set => OnPropertyChanged(ref _previews, value);
        }

        private List<string> _preset_sizes;
        public List<string> Preset_Sizes
        {
            get => _preset_sizes;
            set => OnPropertyChanged(ref _preset_sizes, value);
        }

        public ResizerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;

            Previews = new()
            {
                new ImageProperties()
                {
                    FileName = "default",
                    ImageUrl = "",
                    FileSize = 1024,
                    Width = 50,
                    Height = 50,
                    LastUpdated = DateTime.Now
                },
                new ImageProperties()
                {
                    FileName = "default_2",
                    ImageUrl = "",
                    FileSize = 1024,
                    Width = 50,
                    Height = 50,
                    LastUpdated = DateTime.Now
                },
            };
            Preset_Sizes = new()
            {
                "50x50",
                "100x100",
                "450x450",
                "1000x1000"
            };
            
        }

        private void OnSelectedViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
