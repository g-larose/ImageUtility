using SixLabors.ImageSharp.Formats;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface IResizer
    {
        Task<bool> ResizeAsync(string file, int height, int width, string copyToDir);
    }
}
