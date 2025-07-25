namespace LibraryManagement.Application.DTOs
{
    public class MemberVisitDto
    {
        public int VisitId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public VisitPurpose Purpose { get; set; }
        public string? Notes { get; set; }
        public string? AreaAccessed { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateMemberVisitDto
    {
        public int MemberId { get; set; }
        public VisitPurpose Purpose { get; set; } = VisitPurpose.Reading;
        public string? Notes { get; set; }
        public string? AreaAccessed { get; set; }
    }

    public class CheckOutDto
    {
        public int VisitId { get; set; }
        public string? Notes { get; set; }
    }

    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int? MemberId { get; set; }
        public string? MemberName { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public bool IsGlobal { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? Priority { get; set; }
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
        public string? ActionUrl { get; set; }
        public string? ActionText { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateNotificationDto
    {
        public int? MemberId { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsGlobal { get; set; } = false;
        public DateTime? ScheduledFor { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? Priority { get; set; } = "Normal";
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
        public string? ActionUrl { get; set; }
        public string? ActionText { get; set; }
    }

    public class RoomDto
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string? Equipment { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool RequiresApproval { get; set; }
        public int MaxReservationHours { get; set; }
        public int AdvanceBookingDays { get; set; }
        public string? Rules { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoomReservationDto
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationStatus Status { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public int ExpectedAttendees { get; set; }
        public string? SpecialRequirements { get; set; }
        public string? Notes { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int? ApprovedByLibrarianId { get; set; }
        public string? ApprovedByLibrarianName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public decimal? TotalCost { get; set; }
        public bool IsPaid { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsOverdue { get; set; }
    }

    public class CreateRoomReservationDto
    {
        public int RoomId { get; set; }
        public int MemberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public int ExpectedAttendees { get; set; } = 1;
        public string? SpecialRequirements { get; set; }
        public string? Notes { get; set; }
    }

    public class LibraryAssetDto
    {
        public int AssetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AssetTag { get; set; } = string.Empty;
        public AssetType Type { get; set; }
        public AssetStatus Status { get; set; }
        public string PhysicalLocation { get; set; } = string.Empty;
        public int? ShelfId { get; set; }
        public string? ShelfCode { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpiry { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class ShelfDto
    {
        public int ShelfId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Section { get; set; }
        public int Level { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int AvailableSpace => MaxCapacity - CurrentOccupancy;
    }
}