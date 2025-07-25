using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Notification : BaseAuditableEntity
    {
        [Key]
        public int NotificationId { get; set; }

        public int? MemberId { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public bool IsGlobal { get; set; } = false;

        public DateTime? ReadAt { get; set; }

        public DateTime? ScheduledFor { get; set; }

        public DateTime? ExpiresAt { get; set; }

        [StringLength(50)]
        public string? Priority { get; set; } = "Normal";

        // Related entity information
        public int? RelatedEntityId { get; set; }

        [StringLength(50)]
        public string? RelatedEntityType { get; set; }

        [StringLength(1000)]
        public string? ActionUrl { get; set; }

        [StringLength(50)]
        public string? ActionText { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Member? Member { get; set; }
    }
}