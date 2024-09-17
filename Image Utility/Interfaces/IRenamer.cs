using Image_Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface IRenamer
    {
        Task<bool> RenameFileAsync(string filePath);
        Task<bool> RenameFilesAsync(string[] files, string destFilePath, RenamOptions options, bool useExternal);
    }
}
