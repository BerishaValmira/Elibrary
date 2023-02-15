using System.ComponentModel.DataAnnotations;
using LibraryMngSys.Models.Book;

namespace LibraryMngSys.Models.Category
{
    public class Category 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IEnumerable<Book.Book>? Books { get; set; }

    }
}
