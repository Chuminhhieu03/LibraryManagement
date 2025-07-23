using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public enum BookStatus
    {
        Available,
        Borrowed,
        Reserved,
        Lost,
        Damaged,
        UnderMaintenance
    }

    public class Book
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
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
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