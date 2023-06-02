using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Helpers
{
    public static class EnvironmentHelper
    {
        public static string ImagePath = "images";
        //public static string VideoPath = "videos";
        public static string WebRootPath { get; set; }
        public static string AttachmentImagePath => Path.Combine(WebRootPath, ImagePath);
        public static string ExcelRootPath => Path.Combine(WebRootPath, "excels");
        public static string ExcelPath => "excels";
        //public static string AttachmentVideoPath => Path.Combine(WebRootPath, VideoPath);
    }
}
