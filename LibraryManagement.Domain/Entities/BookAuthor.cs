using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
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