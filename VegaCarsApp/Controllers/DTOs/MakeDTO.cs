using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace VegaCarsApp.Controllers.DTOs
{
    public class MakeDTO :  KeyValuePairDTO
    {
        public ICollection<KeyValuePairDTO> Models { get; set; }
        
        public MakeDTO()
        {
            //collection ne permite sa accesam un obiecte dintr-o lista prin index
            Models = new Collection<KeyValuePairDTO>();
        }
    }
}