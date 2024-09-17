using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Models
{
    public class RenamOptions
    {
        public string? MatchFor { get; set; }
        public string? ReplaceWith { get; set; }
        public string? OldExt { get; set; }
        public string? NewExt { get; set; }

    }
}
