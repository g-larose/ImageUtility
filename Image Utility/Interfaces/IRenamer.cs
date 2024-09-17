using Image_Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface IRenamer
    {
        Task<bool> RenameFile(string filePath);
        Task<bool> RenameFiles(string[] files, string destDir, RenamOptions options, bool useExternal);
    }
}
