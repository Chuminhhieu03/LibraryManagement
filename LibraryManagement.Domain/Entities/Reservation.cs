// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the various statuses a reservation can have within the system.
    /// </summary>
    /// <remarks>
    /// This enumeration is used to indicate the current state of a reservation.
    /// It applies to different types of reservations, such as book reservations or room reservations.
    /// </remarks>
    public enum ReservationStatus
    {
        Active,
        Fulfilled,
        Cancelled,
        Expired
    }

    /// <summary>
    /// Represents a reservation made by a library member for a specific book.
    /// </summary>
    /// <remarks>
    /// This class includes details about the reservation such as its status, related book and member,
    /// as well as dates such as when the reservation was created, fulfilled, or when it expires.
    /// </remarks>
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