using Image_Utility.Commands;
using Image_Utility.Interfaces;
using Image_Utility.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Image_Utility.ViewModels
{
    public class ResizerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;

        public ICommand SetSourceDirCommand { get; }
        public ICommand SetDestinationDirCommand { get; }

        #region PROPERTIES

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

        private string _sourceDir;
        public string SourceDir
        {
            get => _sourceDir;
            set => OnPropertyChanged(ref _sourceDir, value);
        }

        private string _destinationDir;
        public string DestinationDir
        {
            get => _destinationDir;
            set => OnPropertyChanged(ref _destinationDir, value);
        }

        #endregion

        public ResizerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
            SetSourceDirCommand = new RelayCommand(SetSourceDir);
            SetDestinationDirCommand = new RelayCommand(SetDestinationDir);

            LoadPresets();
            
            
        }

        #region PRIVATE METHODS

        private void SetSourceDir()
        {
            var ofd = new FolderBrowserDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceDir = ofd.SelectedPath;
                LoadPreview();
            }
        }

        private void SetDestinationDir()
        {
            var ofd = new FolderBrowserDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DestinationDir = ofd.SelectedPath;
            }
        }

        private void LoadPreview()
        {
            var files = Directory.GetFiles(SourceDir);
            Previews = new();
            for (int i = 0; i < files.Length; i++)
            {
                var file = new FileInfo(files[i]);
                var fileSize = file.Length;
                var fSize = FileSizeFormatter.FormatSize(fileSize);
                var img = Image.FromFile(file.FullName);
                Previews.Add(
                new ImageProperties()
                {
                    FileName = new FileInfo(files[i]).Name,
                    ImageUrl = files[i],
                    FileSize = fSize,
                    Size = $"{img.Height}x{img.Width}",
                    LastUpdated = file.LastWriteTime.ToShortDateString(),
                });
            }
        }

        private void LoadPresets()
        {
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

        #endregion
    }
}
