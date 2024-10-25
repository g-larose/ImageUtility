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
        private readonly ILogger _logger;
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => OnPropertyChanged(ref _errorMessage, value);
        }

        #endregion

        public ResizerViewModel(INavigator? navigator, ILogger logger)
        {
            _navigator = navigator;
            _logger = logger;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
            SetSourceDirCommand = new AsyncRelayCommand(SetSourceDir, null);
            SetDestinationDirCommand = new RelayCommand(SetDestinationDir);

            LoadPresets();
            
            
        }

        #region PRIVATE METHODS

        private async Task SetSourceDir()
        {
            var ofd = new FolderBrowserDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceDir = ofd.SelectedPath;
                await LoadPreview();
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

        private async Task LoadPreview()
        {
            var files = Directory.GetFiles(SourceDir);
            Previews = new();

            await Task.Run(async () =>
            {

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        var file = new FileInfo(files[i]);
                        var fileSize = file.Length;
                        var fSize = FileSizeFormatter.FormatSize(fileSize);
                        var img = Image.FromFile(file.FullName);

                        App.Current.Dispatcher.Invoke((Action)delegate ()
                        {
                            Previews.Add(
                            new ImageProperties()
                            {
                                FileName = new FileInfo(file.Name).Name,
                                ImageUrl = file.FullName,
                                FileSize = fSize,
                                Size = $"{img.Height}x{img.Width}",
                                LastUpdated = file.LastWriteTime.ToShortDateString(),
                            });
                        });
                            
                        }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        _logger.Log(DateTime.Now, ErrorMessage);
                    }
                    
                }
            });
            //return;
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
