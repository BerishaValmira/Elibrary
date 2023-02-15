using System.ComponentModel.DataAnnotations;

namespace LibraryMngSys.Models.Author
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Number { get; set; }
       
    }
}
