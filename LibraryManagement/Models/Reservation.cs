using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public enum ReservationStatus
    {
        Active,
        Fulfilled,
        Cancelled,
        Expired
    }

    public class Reservation
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
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int BookId { get; set; }
        public int MemberId { get; set; }
        
        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}