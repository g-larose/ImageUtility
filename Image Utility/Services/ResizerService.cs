using Image_Utility.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace Image_Utility.Services
{
    public class ResizerService : IResizer
    {
        public async Task<bool> ResizeAsync(string file, int height, int width, string copyTo)
        {
            using (Image image = Image.Load(file))
            {
                image.Mutate(x => x.Resize(width, height));
               
                await Task.Run(() => image.Save(copyTo, new PngEncoder()));
            }
            return true;

        }
    }
}
