using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaCarsApp.Controllers.DTOs
{
    public class VehicleQueryDTO
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }

        public string SortBy {get; set; }

        public bool IsSortAscending { get; set; }
    }
}