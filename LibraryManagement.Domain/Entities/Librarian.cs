// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    public enum LibrarianRole
    {
        Assistant,
        Librarian,
        SeniorLibrarian,
        ChiefLibrarian,
        Administrator
    }

    public class Librarian : BaseEntity
    {
        [Key]
        public int LibrarianId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        public LibrarianRole Role { get; set; } = LibrarianRole.Assistant;

        [StringLength(100)]
        public string? Department { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Salary { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<BookLoan> BookLoansProcessed { get; set; } = new List<BookLoan>();
        public virtual ICollection<Fine> FinesProcessed { get; set; } = new List<Fine>();
    }
}