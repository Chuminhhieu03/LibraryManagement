using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Room : BaseAuditableEntity
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        public RoomType Type { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;

        [Range(1, 100)]
        public int Capacity { get; set; } = 1;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? Equipment { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? HourlyRate { get; set; }

        public bool RequiresApproval { get; set; } = false;

        [Range(0, 24)]
        public int MaxReservationHours { get; set; } = 4;

        [Range(0, 30)]
        public int AdvanceBookingDays { get; set; } = 7;

        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        public string? Rules { get; set; }

        [StringLength(200)]
        public string? ContactInfo { get; set; }

        // Navigation properties
        public virtual ICollection<RoomReservation> Reservations { get; set; } = new List<RoomReservation>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public class RoomReservation : BaseAuditableEntity
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

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

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

        public bool IsPaid { get; set; } = false;

        public DateTime? PaymentDate { get; set; }

        // Computed properties
        public TimeSpan Duration => EndTime.Subtract(StartTime);
        public bool IsOverdue => DateTime.UtcNow > EndTime && Status == ReservationStatus.InProgress;

        // Navigation properties
        public virtual Room Room { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual Librarian? ApprovedByLibrarian { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}