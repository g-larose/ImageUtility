using Image_Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class PageViewModel: ViewModelBase
    {
        private ApplicationPageNames _pageName;

        public ApplicationPageNames PageName
        {
            get => _pageName;
            set => OnPropertyChanged(ref _pageName, value);
        }
    }
}
