using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Danciu_Radu_Lab2.Models
{
        public class Book
        {
            public int ID { get; set; }
            [Required(ErrorMessage = "Title is required")]
            [StringLength(150, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 150 characters")]
            [Display(Name = "Book Title")]
            public string Title { get; set; }

            [Column(TypeName = "decimal(6, 2)")]
            [Range(0.01, 500)]

            public decimal Price { get; set; }

            [DataType(DataType.Date)]
            public DateTime PublishingDate { get; set; }
            public int? PublisherID { get; set; }
            public Publisher? Publisher { get; set; }
            public int? AuthorID { get; set; }
            public Author? Author { get; set; }
            public int? BorrowingID { get; set; }
            public ICollection<Borrowing>? Borrowing { get; set; }

            public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
