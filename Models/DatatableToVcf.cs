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
        public static string ConvertDatatableToVcf(DataTable dt, string fName)
        {
            var myCard = new vCard();
            int num;
            if(System.IO.Directory.Exists("NewVCF"))
                System.IO.Directory.Delete("NewVCF",true);
            System.IO.Directory.CreateDirectory("NewVCF");
            if (System.IO.File.Exists("NewVCF\\" + fName + ".vcf"))
            {
                System.IO.File.Delete("NewVCF\\" + fName + ".vcf");
            }
            try
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    num = 0;
                    myCard.Title = dataRow.ItemArray[num++].ToString();                    
                    myCard.FirstName = dataRow.ItemArray[num++].ToString();                    
                    myCard.LastName = dataRow.ItemArray[num++].ToString();                    
                    myCard.Phone = Convert.ToString(dataRow.ItemArray[num++]);                    
                    myCard.Mobile = Convert.ToString(dataRow.ItemArray[num]);
                    using (var file = System.IO.File.Open(Directory.GetCurrentDirectory() + "\\NewVCF" + "\\" + fName + ".vcf", FileMode.Append))
                    using (var writer = new StreamWriter(file))
                    {
                        writer.Write(myCard.ToString());
                    }
                   /* foreach (var item in dataRow.ItemArray)
                    {
                        System.Diagnostics.Debug.WriteLine(item);
                    }*/
                }
                return fName+".vcf";
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }            
            return fName + ".vcf";
        }
    }
}
