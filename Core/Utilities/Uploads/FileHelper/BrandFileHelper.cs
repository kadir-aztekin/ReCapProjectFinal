using System;
using System.IO;
using System.Threading;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Uploads.FileHelper
{
    public class BrandFileHelper
    {


        static string brandDirectory = Directory.GetCurrentDirectory() + @"\wwwroot\";
        static string brandPath = @"/BrandLogoImages\";

        public static string AddBrand(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string newBrandFileName = Guid.NewGuid().ToString("N") + extension;

            if (!Directory.Exists(brandDirectory + brandPath))
            {
                Directory.CreateDirectory(brandDirectory + brandPath);
            }

            using (FileStream fileStream = File.Create(brandDirectory + brandPath + newBrandFileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }


            return (brandPath + newBrandFileName).Replace("\\", "/");
        }



        public static void Delete(string imagePath)
        {
            if (File.Exists(brandDirectory + imagePath.Replace("/", "\\")) && Path.GetFileName(imagePath) != "image.bmp")
            {
                File.Delete(brandDirectory + imagePath.Replace("/", "\\"));
            }
        }


        public static string Update(IFormFile file, string oldImagePath)
        {
            Delete(oldImagePath);
            return AddBrand(file);


        }

    }
}

