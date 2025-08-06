using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents the status of a member within the library management system.
    /// </summary>
    /// <remarks>
    /// The possible statuses are:
    /// - Active: The member is in good standing and can access library services.
    /// - Inactive: The member is temporarily not active.
    /// - Suspended: The member is suspended due to policy violations or other reasons.
    /// - Expired: The member's membership has expired and needs renewal.
    /// </remarks>
    public enum MemberStatus
    {
        Active,
        Inactive,
        Suspended,
        Expired
    }

    /// <summary>
    /// Represents the type of membership available within the library management system.
    /// </summary>
    /// <remarks>
    /// The possible types include:
    /// - Student: Membership designed for students with specific privileges.
    /// - Staff: Membership for library or organizational staff users.
    /// - Premium: Membership offering enhanced services and benefits.
    /// </remarks>
    public enum MembershipType
    {
        Student,
        Staff,
        Premium
    }

    /// <summary>
    /// Represents a library member within the library management system.
    /// </summary>
    /// <remarks>
    /// This class contains essential details about a member, including personal
    /// information, contact details, membership status, and associated library interactions.
    /// </remarks>
    public class Member : BaseEntity
    {
        [Key]
        public int MemberId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string MembershipNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Phone { get; set; }
        
        [StringLength(500)]
        public string? Address { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? ExpiryDate { get; set; }
        
        //public MembershipType MembershipType { get; set; } = MembershipType.General;
        
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        
        [Range(0, int.MaxValue)]
        public int MaxBooksAllowed { get; set; } = 5;
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalFines { get; set; } = 0;
        
        // Navigation properties
        public virtual ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
        public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual ICollection<MemberVisit> Visits { get; set; } = new List<MemberVisit>();
        public virtual ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}