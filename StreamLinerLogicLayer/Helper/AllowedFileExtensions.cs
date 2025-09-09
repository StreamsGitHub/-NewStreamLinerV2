using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Helper
{
    public class AllowedFileExtensions
    {
        // Office documents
        public static readonly string[] Documents =
        {
            ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf",
            ".txt", ".rtf", ".csv"
        };

        // Images
        public static readonly string[] Images =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".webp", ".svg"
        };

        // Audio
        public static readonly string[] Audio =
        {
            ".mp3", ".wav", ".wma", ".aac", ".flac", ".ogg", ".m4a"
        };

        // Video
        public static readonly string[] Video =
        {
            ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm", ".mpeg", ".mpg"
        };

        // Combined list
        public static readonly string[] All =
            Documents
            .Concat(Images)
            .Concat(Audio)
            .Concat(Video)
            .ToArray();
    }
}

