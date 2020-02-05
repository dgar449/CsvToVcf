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
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            DataTable dTable;
            string fPath = CSVManipulation.uploadToServer(file);
            dTable = CsvToDatatable.ConvertCSVtoDataTable(fPath);   
            vcfFileName=DatatableToVcf.ConvertDatatableToVcf(dTable, Path.GetFileNameWithoutExtension(fPath));
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
            return File(memory, fileType.GetContentType(path), Path.GetFileName(path));
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
