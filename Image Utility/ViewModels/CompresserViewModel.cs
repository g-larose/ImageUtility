﻿using Image_Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.ViewModels
{
    public class CompresserViewModel: ViewModelBase
    {
        private readonly INavigator? _navigator;

        public CompresserViewModel(INavigator? navigator)
        {
            _navigator = navigator;
        }
    }
}
