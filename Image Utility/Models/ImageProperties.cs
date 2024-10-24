using System;

namespace Image_Utility.ViewModels
{
    public class ImageProperties
    {
        public string? FileName { get; set; }
        public string? ImageUrl { get; set; }
        public int FileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
}
