using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Text;
using System;

namespace CsvToVcf.Controllers
{
    public class Default1Controller : Controller
    {
        public IActionResult Index()
        {
            string prefix, fName, lName;
            int phone, mPhone;
            DataTable dTable;

            string fPath = Directory.GetCurrentDirectory() + "\\SimpleData.csv";
            //"C:\\Users\\Admin\\Documents\\Visual Studio 2019\\Trial\\CsvToVcf\\SimpleData.csv"             
            dTable = ConvertCSVtoDataTable(fPath);
            var myCard = new VCard();
            System.Diagnostics.Debug.WriteLine(dTable.Rows.Count);
            int num;
            foreach (DataRow dataRow in dTable.Rows)
            {
                num = 0;
                System.Diagnostics.Debug.WriteLine(dataRow.ItemArray.Length+"fff");
                myCard.title = dataRow.ItemArray[num].ToString();
                num++;
                myCard.FirstName = dataRow.ItemArray[num].ToString();
                num++;
                myCard.LastName = dataRow.ItemArray[num].ToString();
                num++;
                myCard.Phone = Convert.ToString(dataRow.ItemArray[num]);
                num++;
                myCard.Mobile = Convert.ToString(dataRow.ItemArray[num]);

                using (var file = System.IO.File.Open(Directory.GetCurrentDirectory() + "\\me.vcf",FileMode.Append))
                using (var writer = new StreamWriter(file))
                {
                    writer.Write(myCard.ToString());
                }
                foreach (var item in dataRow.ItemArray)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
            //for (int i = 0; i < dTable.Rows.Count; i++)
            //{
            //    myCard.FirstName = dTable.Rows.;
            //};
            //dTable.Rows[0]
            return View(dTable);
        }
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
    }
    public class VCard
    {
        public string title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string StreetAddress { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public byte[] Image { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:2.1");
            builder.Append("Prefix:" + title);
            // Name
            builder.AppendLine("N:" + LastName + ";" + FirstName);
            // Full name
            builder.AppendLine("FN:" + FirstName + " " + LastName);
            // Address
            builder.Append("ADR;HOME;PREF:;;");
            builder.Append(StreetAddress + ";");
            builder.Append(City + ";;");
            builder.Append(Zip + ";");
            builder.AppendLine(CountryName);
            // Other data
            builder.AppendLine("ORG:" + Organization);
            builder.AppendLine("TITLE:" + JobTitle);
            builder.AppendLine("TEL;HOME;VOICE:" + Phone);
            builder.AppendLine("TEL;CELL;VOICE:" + Mobile);
            builder.AppendLine("URL;" + HomePage);
            builder.AppendLine("EMAIL;PREF;INTERNET:" + Email);
            builder.AppendLine("END:VCARD");
            return builder.ToString();
        }
    }
}