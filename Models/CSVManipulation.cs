using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CsvToVcf.Models
{
    public class CSVManipulation
    {
        public static string uploadToServer(IFormFile file)
        {
            try 
            {
            // Extract file name from whatever was posted by browser
            Random rnd = new Random();
            var fileName = System.IO.Path.GetFileName(file.FileName);
            var newFileName = rnd.Next(100, 999) + fileName;

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            if (System.IO.Directory.Exists("UploadedCSV"))
                System.IO.Directory.Delete("UploadedCSV", true);
            System.IO.Directory.CreateDirectory("UploadedCSV");

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
                System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory() + "\\" + fileName);

            }
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Copy(Directory.GetCurrentDirectory() + "\\" + fileName, Directory.GetCurrentDirectory() + "\\UploadedCSV\\" + newFileName);
            }
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            //ViewBag.Message = "File successfully uploaded";
            System.Diagnostics.Debug.WriteLine("This is sparta");
            string fPath = Directory.GetCurrentDirectory() + "\\UploadedCSV\\" + newFileName;

            return fPath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "Oops that didn't work!";
        }
    }
}
