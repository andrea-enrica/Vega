using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VegaCarsApp.Controllers.DTOs
{
    public class SaveVehicleDTO
    {

        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactDTO Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleDTO()
        {
            Features = new Collection<int>();
        }
    }
}