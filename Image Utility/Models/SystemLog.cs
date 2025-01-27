using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image_Utility.Enums;

namespace Image_Utility.Models
{
    public class SystemLog
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime Timestamp { get; set; }
        public LogType LogType { get; set; }
    }
}
