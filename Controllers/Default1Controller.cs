using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Text;
using System;
using CsvToVcf.Models;

namespace CsvToVcf.Controllers
{
    public class Default1Controller : Controller
    {
        public IActionResult Index()
        {
            DataTable dTable,d2;
            string fPath = Directory.GetCurrentDirectory() + "\\SimpleData.csv";
            dTable = CsvToDatatable.ConvertCSVtoDataTable(fPath);
            System.Diagnostics.Debug.WriteLine(dTable.Rows.Count);
            d2=DatatableToVcf.ConvertDatatableToVcf(dTable);
            return View(d2);
        }
    }
    
}