// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents a category within the library management system.
    /// </summary>
    /// <remarks>
    /// A Category entity is used to group books with similar themes, topics, or characteristics.
    /// It contains details like the category name, description, and the collection of books that belong to the category.
    /// </remarks>
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation properties
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}