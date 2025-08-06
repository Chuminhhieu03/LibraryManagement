// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the various statuses of a loan in the library management system.
    /// </summary>
    /// <remarks>
    /// This enumeration defines the possible states of a book loan from the moment it is issued
    /// to the point it is returned or marked as lost. It helps in tracking and managing loans
    /// effectively within the library.
    /// </remarks>
    /// <value>
    /// - Active: Indicates the loan is currently active and the book has not yet been returned.
    /// - Returned: Indicates the book associated with the loan has been returned by the borrower.
    /// - Overdue: Indicates the loan is overdue and the scheduled return date has passed.
    /// - Lost: Indicates the borrower has declared or the system has deemed the book as lost.
    /// - Renewed: Indicates the loan has been renewed for an extended period.
    /// </value>
    public enum LoanStatus
    {
        Active,
        Returned,
        Overdue,
        Lost,
        Renewed
    }

    /// <summary>
    /// Represents the loan of a book to a library member, including the details of the loan period, status, and related entities.
    /// </summary>
    public class BookLoan : BaseEntity
    {
        [Key]
        public int LoanId { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Active;

        [Range(0, int.MaxValue)]
        public int RenewalCount { get; set; } = 0;

        [Range(0, 10)]
        public int MaxRenewalsAllowed { get; set; } = 3;

        [StringLength(500)]
        public string? Notes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? FineAmount { get; set; } = 0;
        // Foreign Keys
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int? ProcessedByLibrarianId { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual Librarian? ProcessedByLibrarian { get; set; }
        public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();
    }
}