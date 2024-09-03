using Image_Utility.Commands;
using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private List<File> _files;
        public List<File> Files
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
        public RenamerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            OpenSourceFolderCommand = new RelayCommand(OpenFileBrowser);
            SetDestinationFolderCommand = new RelayCommand(SetDestinationDir);
            Files = new();
            Files.Add(new File { fileName = "imageOne.jpg", newFileName = "image-001.png" });
           
        }

        private void SetDestinationDir()
        {
            throw new NotImplementedException();
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
            ofd.InitialDirectory = @"F:";//Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceDir = ofd.SelectedPath;
                var fCount = Directory.GetFiles(SourceDir).Length;
                FileCount = fCount;
            }
            
        }
    }
}
