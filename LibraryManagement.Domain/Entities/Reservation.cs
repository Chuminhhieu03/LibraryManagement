// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    public enum ReservationStatus
    {
        Active,
        Fulfilled,
        Cancelled,
        Expired
    }

    public class Reservation : BaseEntity
    {
        [Key]
        public int ReservationId { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public DateTime ExpiryDate { get; set; }

        public DateTime? FulfilledDate { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Active;

        [Range(1, int.MaxValue)]
        public int QueuePosition { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Foreign Keys
        public int BookId { get; set; }
        public int MemberId { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}