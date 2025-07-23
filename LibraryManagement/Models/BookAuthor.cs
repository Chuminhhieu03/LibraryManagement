using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    // Junction table for Many-to-Many relationship between Book and Author
    public class BookAuthor
    {
        [Key]
        public int BookAuthorId { get; set; }
        
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        
        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Author Author { get; set; } = null!;
    }
}