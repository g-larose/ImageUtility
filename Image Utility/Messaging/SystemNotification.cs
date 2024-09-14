using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Messaging
{
    public class SystemNotification
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? IconPath { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }

        public SystemNotification(string? title, string? message, string? iconPath, int locationX, int locationY)
        {
            Title = title;
            Message = message;
            IconPath = iconPath;
            LocationX = locationX;
            LocationY = locationY;
        }
    }
}
