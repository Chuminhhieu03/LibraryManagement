using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the status of a fine in the library management system.
    /// </summary>
    /// <remarks>
    /// This enum is used to indicate the current state of a fine, allowing the system
    /// to track its resolution status. Typical states include pending, paid, waived,
    /// and cancelled.
    /// </remarks>
    public enum FineStatus
    {
        Pending,
        Paid,
        Waived,
        Cancelled
    }

    /// <summary>
    /// Represents the type of a fine in the library management system.
    /// </summary>
    /// <remarks>
    /// This enum classifies fines based on their reason, such as overdue books, lost items,
    /// or other issues. It helps in categorizing and managing fines effectively within the system.
    /// </remarks>
    public enum FineType
    {
        OverdueBook,
        LostBook,
        DamagedBook,
        LateReturn,
        Other
    }

    /// <summary>
    /// Represents a fine imposed within the library management system.
    /// </summary>
    /// <remarks>
    /// A fine is typically associated with a specific library member and optionally linked to a book loan.
    /// Fines may be issued for various purposes, such as overdue books, lost or damaged books, and other policy violations.
    /// Each fine includes information about its status, type, amount, and additional details.
    /// </remarks>
    public class Fine : BaseEntity
    {
        [Key]
        public int FineId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public FineType Type { get; set; } = FineType.OverdueBook;

        public FineStatus Status { get; set; } = FineStatus.Pending;

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? PaymentDate { get; set; }

        public DateTime? DueDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Foreign Keys
        public int MemberId { get; set; }
        public int? BookLoanId { get; set; }
        public int? ProcessedByLibrarianId { get; set; }

        // Navigation properties
        public virtual Member Member { get; set; } = null!;
        public virtual BookLoan? BookLoan { get; set; }
        public virtual Librarian? ProcessedByLibrarian { get; set; }
    }
}