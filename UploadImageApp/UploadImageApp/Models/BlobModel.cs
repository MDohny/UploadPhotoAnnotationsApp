using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UploadImageApp.Models
{
    public static class BlobModel
    {
        private static string imagePath;
        public static string ImagePath { get { return imagePath; } set { imagePath = value; } }

        private static string annotations;
        public static string Annotations { get { return annotations; } set { annotations = value; } }
    }
}
