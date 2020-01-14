﻿using System;
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
using Microsoft.AspNetCore.Http;

namespace CsvToVcf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        System.Diagnostics.Debug.WriteLine("Yo your file uploaded!!");
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
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
