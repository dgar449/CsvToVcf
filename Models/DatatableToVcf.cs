using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace CsvToVcf.Models
{
    public class DatatableToVcf
    {
        public static DataTable ConvertDatatableToVcf(DataTable d)
        {
            var myCard = new vCard();
            int num;
            foreach (DataRow dataRow in d.Rows)
            {
                num = 0;
                System.Diagnostics.Debug.WriteLine(dataRow.ItemArray.Length+"fff");
                myCard.Title = dataRow.ItemArray[num].ToString();
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

                var datre = DateTime.Now;
            }
            return d;
        }
    }
}
