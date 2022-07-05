using System.ComponentModel.DataAnnotations;

namespace VegaCarsApp.Controllers.DTOs
{
    public partial class SaveVehicleDTO
    {
        public class ContactDTO {
            [Required]
            [StringLength(255)]
            public string Name { get; set; }

            [StringLength(255)]
            public string Email { get; set; }

            [Required]
            [StringLength(255)]
            public string Phone { get; set; }
        }
    }
}