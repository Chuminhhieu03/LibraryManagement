using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between a book and an author in the library management system.
    /// </summary>
    /// <remarks>
    /// This entity acts as a junction table for a many-to-many relationship between books and authors.
    /// It contains foreign keys referencing the Book and Author entities.
    /// </remarks>
    public class BookAuthor : BaseEntity
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