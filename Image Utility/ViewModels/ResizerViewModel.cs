using Image_Utility.Commands;
using Image_Utility.Interfaces;
using Image_Utility.Services;
using Image_Utility.Utilities;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Image_Utility.Enums;
using Image_Utility.Models;
using static Image_Utility.App;
using Application = System.Windows.Application;

namespace Image_Utility.ViewModels
{
    public class ResizerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        private readonly ILogger _logger;
        public AppViewModel AppViewModel;

        private ResizerService _resizerService = new();
        public ViewModelBase? SelectedViewModel => _navigator!.CurrentViewModel;

       
        public ICommand SetSourceDirCommand { get; }
        public ICommand SetDestinationDirCommand { get; }
        public ICommand DoResizeCommand { get; }

        #region PROPERTIES

        private ObservableCollection<ImageProperties> _previews;
        public ObservableCollection<ImageProperties> Previews
        {
            get => _previews;
            set => OnPropertyChanged(ref _previews, value);
        }

        private List<string> _files;
        public List<string> Files
        {
            get => _files;
            set => OnPropertyChanged(ref _files, value);
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

        private bool _cExecute;
        public bool CanExecute
        {
            get => _cExecute;
            set => OnPropertyChanged(ref _cExecute, value);
        }

        private int _width;
        public int Width
        {
            get => _width;
            set => OnPropertyChanged(ref _width, value);
        }

        private int _height;
        public int Height
        {
            get => _height;
            set => OnPropertyChanged(ref _height, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => OnPropertyChanged(ref _isRunning, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => OnPropertyChanged(ref _progressValue, value);
        }

        #endregion

        public ResizerViewModel(INavigator? navigator, ILogger logger, AppViewModel vm)
        {
            _navigator = navigator;
            _logger = logger;
            AppViewModel = vm;
            _navigator!.CurrentViewModelChanged += OnSelectedViewModelChanged;
            SetSourceDirCommand = new AsyncRelayCommand(SetSourceDir, null);
            SetDestinationDirCommand = new RelayCommand(SetDestinationDir);
            DoResizeCommand = new AsyncRelayCommand(DoResize, null);
            _logger.Log(DateTime.Now, "Loaded Resizer View", LogType.Information, TargetType.File);
            Previews = new();
            BindingOperations.EnableCollectionSynchronization(Previews, new object());
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

        private async Task DoResize()
        {
            
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp"));

            IsRunning = true;

            for (int i = 0; i < Files.Count; i++)
            {
                var file = Files[i];
                var copyTo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp", $"temp_{i}.png");
                await _resizerService.ResizeAsync(file, 55, 300, copyTo);
            }
            _logger.Log(DateTime.Now, "DoResize() method called", LogType.Information, TargetType.File);

        }

        #region LOAD PREVIEWS
        private async Task LoadPreview()
        {
            _logger.Log(DateTime.Now, "LoadPreview() method called", LogType.Information, TargetType.File);
            var files = Directory.GetFiles(SourceDir);
            Files = files.ToList();
            AppViewModel.CanExecute = false;
            await Task.Run(() =>
            {

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        var file = new FileInfo(files[i]);
                        var fileSize = file.Length;
                        var fSize = FileSizeFormatter.FormatSize((int)fileSize);
                        var img = Image.FromFile(file.FullName);

                        Application.Current.Dispatcher.Invoke((Action)delegate ()
                        {
                            Previews.Add(
                            new ImageProperties()
                            {
                                FileName = new FileInfo(file.Name).FullName,
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
                        _logger.Log(DateTime.Now, ErrorMessage, LogType.Information, TargetType.File);
                    }
                    
                }
            });
            AppViewModel.CanExecute = true;
            //return;
        }

        #endregion

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
            _logger.Log(DateTime.Now, "PropertyChanged event invoked for [ResizerViewModel].", LogType.Information, TargetType.File);
        }

        #endregion
    }
}
