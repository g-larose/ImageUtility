using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Models
{
    public class SystemLog
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public SystemLogLevel LogLevel { get; set; }


    }
}
