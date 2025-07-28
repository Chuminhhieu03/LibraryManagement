using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    public class Shelf : BaseEntity
    {
        [Key]
        public int ShelfId { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Section { get; set; }

        [Range(1, 20)]
        public int Level { get; set; } = 1;

        [Range(0, 1000)]
        public int MaxCapacity { get; set; } = 100;

        [Range(0, 1000)]
        public int CurrentOccupancy { get; set; } = 0;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<LibraryAsset> Assets { get; set; } = new List<LibraryAsset>();
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}