
    using System.IO;
    using Microsoft.AspNetCore.Http;

namespace StudentManagementSystem.Utils
{
    public class FileHelper
    {
        public static string FileLoader(IFormFile formFile, string filePath = "/pdf/")
        {
            var fileName = "";

            if (formFile != null && formFile.Length > 0)
            {
                fileName = formFile.FileName;
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;
                using (var stream = new FileStream(directory, FileMode.Create))
                {

                    formFile.CopyTo(stream);
                }
            }

            return fileName;
        }

        public static bool FileTerminator(string fileName, string filePath = "/Img/")
        {
            string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;

            if (File.Exists(directory)) 
            {
                File.Delete(directory);
                return true;
            }

            return false;
        }

    }
}