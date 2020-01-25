using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvToVcf.Models
{
    public class ReturnObject<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public Exception Exception { get; set; }
        public List<string> Errors { get; set; }
    }
}
