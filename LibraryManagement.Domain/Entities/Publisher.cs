using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents a publisher that provides books in the library management system.
    /// </summary>
    /// <remarks>
    /// This class holds information pertaining to a publisher, including its identifying
    /// details such as name and address, as well as optional contact information like
    /// phone number, email, and website link.
    /// </remarks>
    /// <example>
    /// Publishers are entities that manage and provide books for the system.
    /// Typically, a publisher will have one or more associated books.
    /// </example>
    /// <remarks>
    /// Each publisher is uniquely identified by its PublisherId. This class also contains
    /// navigational properties, enabling the relationship between publishers and their books
    /// to be modeled.
    /// </remarks>
    public class Publisher : BaseEntity
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Website { get; set; }

        // Navigation properties
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}