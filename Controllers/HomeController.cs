using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using CsvToVcf.Models;
using System.IO;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace CsvToVcf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static string vcfFileName;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //System.Diagnostics.Debug.WriteLine("This is sparta");
            //DataTable dTable;
            //string fPath = Directory.GetCurrentDirectory() + "\\SimpleData.csv";
            //dTable = CsvToDatatable.ConvertCSVtoDataTable(fPath);
            //System.Diagnostics.Debug.WriteLine(dTable.Rows.Count);
            //DatatableToVcf.ConvertDatatableToVcf(dTable);
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
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

            ViewBag.Message = "File successfully uploaded";
            System.Diagnostics.Debug.WriteLine("This is sparta");
            DataTable dTable;
            string fPath = Directory.GetCurrentDirectory() + "\\UploadedCSV\\" + newFileName;
            dTable = CsvToDatatable.ConvertCSVtoDataTable(fPath);            
           // System.Diagnostics.Debug.WriteLine(dTable.Rows.Count);
            vcfFileName=DatatableToVcf.ConvertDatatableToVcf(dTable, Path.GetFileNameWithoutExtension(Directory.GetCurrentDirectory() + "\\UploadedCSV\\" + newFileName));

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Download()
        {
            string filename = vcfFileName;
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory() + "\\NewVCF", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".vcf", "text/vcard"},
                {".csv", "text/csv"}
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
