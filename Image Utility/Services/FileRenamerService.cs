using Image_Utility.Interfaces;
using Image_Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Services
{
    public class FileRenamerService : IRenamer
    {
        public async Task<bool> RenameFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RenameFiles(string[] files, string destDir, RenamOptions options, bool useExternal)
        {
            for (int i = 0; i < 6; i++)
            {
                await Task.Delay(1000);
            }

            return true;
        }
    }
}
