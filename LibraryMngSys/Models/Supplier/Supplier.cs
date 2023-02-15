using System.ComponentModel.DataAnnotations;

namespace LibraryMngSys.Models.Supplier
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

    }
}
