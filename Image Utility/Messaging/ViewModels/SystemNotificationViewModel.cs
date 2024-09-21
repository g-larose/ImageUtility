using Image_Utility.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace Image_Utility.Messaging.ViewModels
{
    public class SystemNotificationViewModel: ViewModelBase
    {
        private readonly UserControl _uControl;

        public SystemNotificationViewModel(UserControl uControl)
        {
            _uControl = uControl;
        }
    }
}
