using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaCarsApp.Core.Models
{
    public class PhotoSettings
    {
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName) 
        {
           return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}