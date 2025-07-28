using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    public class RoomReservation : BaseEntity
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Expired;

        [Required]
        [StringLength(200)]
        public string Purpose { get; set; } = string.Empty;

        [Range(1, 100)]
        public int ExpectedAttendees { get; set; } = 1;

        [StringLength(500)]
        public string? SpecialRequirements { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public int? ApprovedByLibrarianId { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        [StringLength(500)]
        public string? CancellationReason { get; set; }

        [Column(TypeName = "decimal(8,2)")]

        public decimal? TotalCost { get; set; }

        // Computed properties
        public TimeSpan Duration => EndTime.Subtract(StartTime);
        public bool IsOverdue => DateTime.UtcNow > EndTime && Status == ReservationStatus.Active;

        // Navigation properties
        public virtual Room Room { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual Librarian? ApprovedByLibrarian { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}