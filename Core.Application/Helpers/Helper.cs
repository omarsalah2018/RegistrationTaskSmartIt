using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Core.Application.Helpers
{
    public static class Helper
    {
        public static async Task<string> UploadFile(IFormFile formData)
        {
            try
            {
                if (formData.Length > 0)
                {
                    IFormFile formFile = formData;
                    string root = "wwwroot";
                    string dir = Path.Combine("Upload", "Files");
                    var folderPath = Path.Combine(root, dir);
                    var fileDate = DateTime.Now.ToString("yyyymmddhhmmss");

                    string filenameWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);

                    string fileExtension = Path.GetExtension(formFile.FileName);

                    string fileNameWithoutSpecialCharacters = RemoveSpecialCharacters(filenameWithoutExtension);

                    var fileNewName = fileNameWithoutSpecialCharacters + "" + fileDate + fileExtension;

                    var filePath = Path.Combine(folderPath, fileNewName);
                    var filePathRelative = Path.Combine(dir, fileNewName);
                    filePathRelative = filePathRelative.Replace("\\", "/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return filePathRelative;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", "", RegexOptions.Compiled);
        }
    }
}
