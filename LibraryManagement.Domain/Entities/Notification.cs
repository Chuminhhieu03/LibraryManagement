using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents a notification entity within the Library Management System.
    /// </summary>
    public class Notification : BaseEntity
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

        public DateTime? ReadAt { get; set; }

        [StringLength(50)]
        public string? Priority { get; set; } = "Normal";

        // Related entity information
        public int? RelatedEntityId { get; set; }

        [StringLength(50)]
        public string? RelatedEntityType { get; set; }

        // Navigation properties
        public virtual Member? Member { get; set; }
    }
}