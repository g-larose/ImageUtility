using Image_Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface IRenamer
    {
        Task<bool> RenameFileAsync(string filePath, string dest, RenamOptions options, [Optional]bool useExternal);
        Task<bool> RenameFilesAsync(string[] files, string destFilePath, RenamOptions options, [Optional]bool useExternal);
    }
}
