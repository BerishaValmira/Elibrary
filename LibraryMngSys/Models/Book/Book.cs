using System.ComponentModel.DataAnnotations;

namespace LibraryMngSys.Models.Book
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Category { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

        public virtual Category.Category? TypeBookCategory { get; set; }

    }
}
