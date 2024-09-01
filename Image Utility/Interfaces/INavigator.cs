using Image_Utility.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface INavigator
    {
        public event Action CurrentViewModelChanged;
        public ViewModelBase? CurrentViewModel { get; set; }
    }
}
