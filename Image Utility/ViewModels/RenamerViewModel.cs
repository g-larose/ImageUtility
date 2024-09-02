using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class RenamerViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;

        private List<File> _files;
        public List<File> Files
        {
            get => _files;
            set => OnPropertyChanged(ref _files, value);
        }
        public RenamerViewModel(INavigator? navigator)
        {
            _navigator = navigator;
            Files = new();
            Files.Add(new File { fileName = "imageOne.jpg", newFileName = "image-001.png" });
           
        }
    }

    public class File
    {
        public string fileName { get; set; }
        public string newFileName { get; set; }
    }
}
