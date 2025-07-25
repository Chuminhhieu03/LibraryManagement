using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Interfaces
{
    public interface IMemberVisitService
    {
        Task<MemberVisitDto> CheckInAsync(CreateMemberVisitDto createDto);
        Task<MemberVisitDto> CheckOutAsync(int visitId, string? notes = null);
        Task<MemberVisitDto?> GetActiveVisitAsync(int memberId);
        Task<IEnumerable<MemberVisitDto>> GetVisitHistoryAsync(int memberId, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<MemberVisitDto>> GetCurrentVisitorsAsync();
        Task<IEnumerable<MemberVisitDto>> GetAllVisitsAsync();
    }

    public interface INotificationService
    {
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createDto);
        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsAsync(int memberId);
        Task<IEnumerable<NotificationDto>> GetGlobalNotificationsAsync();
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(int memberId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int memberId);
        Task DeleteNotificationAsync(int notificationId);
        
        // Automatic notification creation methods
        Task CreateBookOverdueNotificationAsync(int memberId, int bookLoanId);
        Task CreateBookAvailableNotificationAsync(int memberId, int bookId);
        Task CreateMembershipExpiringNotificationAsync(int memberId);
        Task CreateAssetMaintenanceNotificationAsync(int assetId);
        Task CreateRoomAvailableNotificationAsync(int roomId);
        Task CreateSystemMaintenanceNotificationAsync(string message, DateTime scheduledTime);
    }

    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto?> GetRoomByIdAsync(int roomId);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime);
        Task<RoomDto> CreateRoomAsync(RoomDto roomDto);
        Task<RoomDto> UpdateRoomAsync(RoomDto roomDto);
        Task DeleteRoomAsync(int roomId);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeReservationId = null);
    }

    public interface IRoomReservationService
    {
        Task<RoomReservationDto> CreateReservationAsync(CreateRoomReservationDto createDto);
        Task<RoomReservationDto> UpdateReservationAsync(int reservationId, RoomReservationDto reservationDto);
        Task<IEnumerable<RoomReservationDto>> GetMemberReservationsAsync(int memberId);
        Task<IEnumerable<RoomReservationDto>> GetRoomReservationsAsync(int roomId, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<RoomReservationDto>> GetAllReservationsAsync();
        Task<RoomReservationDto?> GetReservationByIdAsync(int reservationId);
        Task CancelReservationAsync(int reservationId, string? reason = null);
        Task ApproveReservationAsync(int reservationId, int librarianId);
        Task CheckInReservationAsync(int reservationId);
        Task CheckOutReservationAsync(int reservationId);
        Task<IEnumerable<RoomReservationDto>> GetOverdueReservationsAsync();
        Task<IEnumerable<RoomReservationDto>> GetPendingApprovalsAsync();
    }

    public interface ILibraryAssetService
    {
        Task<IEnumerable<LibraryAssetDto>> GetAllAssetsAsync();
        Task<LibraryAssetDto?> GetAssetByIdAsync(int assetId);
        Task<IEnumerable<LibraryAssetDto>> GetAssetsByTypeAsync(AssetType type);
        Task<IEnumerable<LibraryAssetDto>> GetAssetsByStatusAsync(AssetStatus status);
        Task<IEnumerable<LibraryAssetDto>> GetAssetsNeedingMaintenanceAsync();
        Task<LibraryAssetDto> CreateAssetAsync(LibraryAssetDto assetDto);
        Task<LibraryAssetDto> UpdateAssetAsync(LibraryAssetDto assetDto);
        Task DeleteAssetAsync(int assetId);
        Task UpdateAssetStatusAsync(int assetId, AssetStatus status);
        Task ScheduleMaintenanceAsync(int assetId, DateTime maintenanceDate, string notes);
    }

    public interface IShelfService
    {
        Task<IEnumerable<ShelfDto>> GetAllShelvesAsync();
        Task<ShelfDto?> GetShelfByIdAsync(int shelfId);
        Task<ShelfDto?> GetShelfByCodeAsync(string code);
        Task<ShelfDto> CreateShelfAsync(ShelfDto shelfDto);
        Task<ShelfDto> UpdateShelfAsync(ShelfDto shelfDto);
        Task DeleteShelfAsync(int shelfId);
        Task<IEnumerable<ShelfDto>> GetAvailableShelvesAsync(int requiredSpace = 1);
    }

    public interface IReportService
    {
        // Visit Reports
        Task<object> GetVisitStatisticsAsync(DateTime from, DateTime to);
        Task<object> GetMemberVisitPatternAsync(int memberId);
        Task<object> GetLibraryUsageStatisticsAsync();
        
        // Asset Reports
        Task<object> GetAssetUtilizationReportAsync();
        Task<object> GetMaintenanceScheduleReportAsync();
        Task<object> GetAssetsByStatusReportAsync();
        
        // Room Reports
        Task<object> GetRoomUtilizationReportAsync(DateTime from, DateTime to);
        Task<object> GetPopularRoomsReportAsync();
        Task<object> GetRoomRevenueReportAsync(DateTime from, DateTime to);
        
        // Notification Reports
        Task<object> GetNotificationStatisticsAsync();
        Task<object> GetUnreadNotificationSummaryAsync();
    }
}