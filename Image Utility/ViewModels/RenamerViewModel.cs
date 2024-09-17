using Image_Utility.Commands;
using Image_Utility.Interfaces;
using Image_Utility.Models;
using Image_Utility.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private ObservableCollection<File> _files;
        public ObservableCollection<File> Files
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
        public RenamerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            OpenSourceFolderCommand = new RelayCommand(OpenFileBrowser);
            SetDestinationFolderCommand = new RelayCommand(SetDestinationDir);
            SetExternalCommand = new RelayCommand(SetExternalFilePath);
            StartRenamingCommand = new AsyncRelayCommand(Rename, (ex) => StatusMessage = ex.Message);
            Files = new();
           
        }

        private async Task Rename()
        {
            var renamerService = new FileRenamerService();
            var options = new RenamOptions() { MatchFor = MatchFor, ReplaceWith = ReplaceWith };

            var files = Directory.GetFiles(SourceDir, ".txt", searchOption: SearchOption.TopDirectoryOnly);
            await renamerService.RenameFiles(files, DestinationDir, options, false);
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

        private void OpenFileBrowser()
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
