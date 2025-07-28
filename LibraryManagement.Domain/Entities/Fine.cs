using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    public enum FineStatus
    {
        Pending,
        Paid,
        Waived,
        Cancelled
    }

    public enum FineType
    {
        OverdueBook,
        LostBook,
        DamagedBook,
        LateReturn,
        Other
    }

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