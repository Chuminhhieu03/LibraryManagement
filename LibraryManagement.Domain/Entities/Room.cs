using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Room : BaseEntity
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
}