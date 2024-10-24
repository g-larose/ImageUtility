using Image_Utility.Commands;
using Image_Utility.Components;
using Image_Utility.Interfaces;
using Image_Utility.Models;
using Image_Utility.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Image_Utility.ViewModels
{
    public class RenamerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;
        public ICommand OpenSourceFolderCommand { get; }
        public ICommand SetDestinationFolderCommand { get; }
        public ICommand SetExternalCommand { get; }
        public ICommand StartRenamingCommand { get; }

        #region Auto Properties
        private ObservableCollection<ImageProperties> _files;
        public ObservableCollection<ImageProperties> Files
        {
            get => _files;
            set => OnPropertyChanged(ref _files, value);
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

        private int _fileCount;
        public int FileCount
        {
            get => _fileCount;
            set => OnPropertyChanged(ref _fileCount, value);
        }

        private string _matchFor;
        public string MatchFor
        {
            get => _matchFor;
            set => OnPropertyChanged(ref _matchFor, value);
        }

        private string _replaceWith;
        public string ReplaceWith
        {
            get => _replaceWith;
            set => OnPropertyChanged(ref _replaceWith, value);
        }

        private string _externalFilePath;
        public string ExternalFilePath
        {
            get => _externalFilePath;
            set => OnPropertyChanged(ref _externalFilePath, value);
        }

        private bool _useExternal;
        public bool UseExternal
        {
            get => _useExternal;
            set => OnPropertyChanged(ref _useExternal, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => OnPropertyChanged(ref _statusMessage, value);
        }

        private string _oldExt;
        public string OldExt
        {
            get => _oldExt;
            set => OnPropertyChanged(ref _oldExt, value);
        }

        private string _newExt;
        public string NewExt
        {
            get => _newExt;
            set => OnPropertyChanged(ref _newExt, value);
        }

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set => OnPropertyChanged(ref _isExecuting, value);
        }

        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set => OnPropertyChanged(ref _progressValue, value);
        }

        private bool _isProgressVisible;
        public bool IsProgressVisible
        {
            get => _isProgressVisible;
            set => OnPropertyChanged(ref _isProgressVisible, value);
        }

        private bool _openAfterExecution;
        public bool OpenAfterExecution
        {
            get => _openAfterExecution;
            set => OnPropertyChanged(ref _openAfterExecution, value);
        }
        #endregion

        public RenamerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            OpenSourceFolderCommand = new RelayCommand(OpenFolderBrowser);
            SetDestinationFolderCommand = new RelayCommand(SetDestinationDir);
            SetExternalCommand = new RelayCommand(SetExternalFilePath);
            StartRenamingCommand = new AsyncRelayCommand(Rename, (ex) => StatusMessage = ex.Message);
            Files = new();

           
        }

        private async Task Rename()
        {
            
            var renamerService = new FileRenamerService();
            var options = new RenamOptions() { MatchFor = MatchFor, ReplaceWith = ReplaceWith, OldExt = OldExt, NewExt = NewExt };

            var files = Directory.GetFiles(SourceDir);
            var fileCount = files.Length;

            for (int i = 0; i < files.Length; i++)
            {
                await renamerService.RenameFileAsync(files[i], DestinationDir, options);
                ProgressValue = ((double)i * 100 / fileCount);
            }

            if (OpenAfterExecution)
                Process.Start("explorer.exe", DestinationDir);

            OpenAfterExecution = false;

            SourceDir = "";
            DestinationDir = "";
            MatchFor = "";
            ReplaceWith = "";
            OldExt = "";
            NewExt = "";
            ProgressValue = 0;

        }

        private void SetExternalFilePath()
        {
            var fd = new OpenFileDialog();
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fd.Filter = "Text Files (*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                ExternalFilePath = fd.FileName;

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

        private void OpenFolderBrowser()
        {
            //var ofd = new OpenFileDialog();
            //ofd.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*";
            //ofd.Multiselect = true;
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if(ofd.ShowDialog() == DialogResult.OK)
            //{
            //    SourceDir = ofd.FileName;
            //}

            var ofd = new FolderBrowserDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceDir = ofd.SelectedPath;
                var fCount = Directory.GetFiles(SourceDir).Length;
                FileCount = fCount;
            }
            
        }
    }
}
