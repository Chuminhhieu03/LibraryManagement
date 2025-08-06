using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the current status of a book in the library management system.
    /// </summary>
    /// <remarks>
    /// The <see cref="BookStatus"/> enumeration defines various states a book can have,
    /// including its availability, condition, or assignment status within the system.
    /// It is utilized to track and manage the operational state of books.
    /// </remarks>
    public enum BookStatus
    {
        Available,
        Borrowed,
        Reserved,
        Lost,
        Damaged,
        UnderMaintenance
    }

    /// <summary>
    /// Represents a book in the library management system.
    /// </summary>
    /// <remarks>
    /// This class is used to store and manage information about books, including their title, ISBN, publication details, status, physical location, and associated entities such as publishers, categories, and authors.
    /// </remarks>
    public class Book : BaseEntity
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [StringLength(50)]
        public string? Language { get; set; }

        [StringLength(200)]
        public string? Edition { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Available;

        [StringLength(500)]
        public string? Location { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual Publisher Publisher { get; set; } = null!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public virtual ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}