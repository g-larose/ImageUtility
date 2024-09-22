using System.Windows;
using System.Windows.Controls;

namespace Image_Utility.Components
{
    public class BorderComponent : Control
    {
        static BorderComponent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderComponent), new FrameworkPropertyMetadata(typeof(BorderComponent)));
        }
    }
}
