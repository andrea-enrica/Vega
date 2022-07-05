using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static VegaCarsApp.Controllers.DTOs.SaveVehicleDTO;

namespace VegaCarsApp.Controllers.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public KeyValuePairDTO Model { get; set; }
        public KeyValuePairDTO Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDTO Contact{ get; set; }
    
        public DateTime LastUpdate { get; set; }

        public ICollection<KeyValuePairDTO> Features { get; set; }

        public VehicleDTO()
        {
            Features = new Collection<KeyValuePairDTO>();
        }
    }
}