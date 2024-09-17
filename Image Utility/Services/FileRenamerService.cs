using Image_Utility.Interfaces;
using Image_Utility.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Image_Utility.Services
{
    public class FileRenamerService : IRenamer
    {
        public async Task<bool> RenameFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RenameFilesAsync(string[] files, string dest, RenamOptions options, [Optional]bool useExternal)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    string file = files[i];
                    int index = file.LastIndexOf("\\");
                    string fileName = file.Substring(index + 1);
                    string newFileName = fileName.Replace(options.MatchFor!, options.ReplaceWith).Replace(options.OldExt!, options.NewExt);

                    await Task.Run(() => File.Copy(file, $"{dest}\\{newFileName}"));
                    string temp = "";
                }
                
            }

            return true;
        }


    }
}
