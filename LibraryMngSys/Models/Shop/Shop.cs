using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryMngSys.Models.Shop
{
    public class Shop 
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Address { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opening Date")]
        public DateTime OpeningDate { get; set; }
    }
}
