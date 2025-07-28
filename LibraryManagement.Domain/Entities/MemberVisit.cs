using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class MemberVisit : BaseEntity
    {
        [Key]
        public int VisitId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;

        public DateTime? CheckOutTime { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        // Computed property for visit duration
        public TimeSpan? Duration => CheckOutTime?.Subtract(CheckInTime);

        // Navigation properties
        public virtual Member Member { get; set; } = null!;
    }
}