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

namespace CsvToVcf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            System.Diagnostics.Debug.WriteLine("This is sparta");
            DataTable dTable;
            string fPath = Directory.GetCurrentDirectory() + "\\SimpleData.csv";
            dTable = CsvToDatatable.ConvertCSVtoDataTable(fPath);
            System.Diagnostics.Debug.WriteLine(dTable.Rows.Count);
            DatatableToVcf.ConvertDatatableToVcf(dTable);
            //return View();
        }
        [HttpPost]
        public ActionResult Upload()
        {
            //if (Request.Files.Count > 0)
            //{
            //    var file = Request.Files[0];

            //    if (file != null && file.ContentLength > 0)
            //    {
            //        var fileName = Path.GetFileName(file.FileName);
            //        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
            //        file.SaveAs(path);
            //    }
            //}

            return RedirectToAction("UploadDocument");
        }

        public IActionResult Index()
        {
            return View();
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
