using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CsvToVcf.Models
{
    public class DownloadVCF
    {
        public async void dlVCF(string filename)
        {
            //if (filename == null)
            //   // /err = "Error";

            var path = Path.Combine(Directory.GetCurrentDirectory() + "\\NewVCF", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
           // File(memory, GetContentType(path), Path.GetFileName(path));
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
    }
}
