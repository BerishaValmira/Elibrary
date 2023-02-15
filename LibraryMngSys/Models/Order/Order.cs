using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMngSys.Models.Order
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("BookId")]
        public Guid BookId { get; set; }

        public virtual Book.Book? book { get; set; }

        [ForeignKey("SupplierId")]
        public Guid SupplierId { get; set; }

        public virtual Supplier.Supplier? Supplier { get; set; }

        public int Amount { get; set; }
    }
}
