using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents an author entity in the library management domain.
    /// </summary>
    /// <remarks>
    /// An author is associated with their respective books through the BookAuthors collection.
    /// </remarks>
    public class Author : BaseEntity
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        // Navigation properties
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}