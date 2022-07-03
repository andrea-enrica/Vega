using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VegaCarsApp.Models
{
    public class Make
    {
        public int id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
        
        public Make()
        {
            //collection ne permite sa accesam un obiecte dintr-o lista prin index
            Models = new Collection<Model>();
        }
    }
}