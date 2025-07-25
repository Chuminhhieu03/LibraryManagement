using System.Linq.Expressions;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Read operations
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        
        // Paging
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageIndex, 
            int pageSize, 
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes);

        // Aggregation
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        
        // Write operations
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        
        // Bulk operations
        Task<int> BulkDeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updateExpression);
    }

    public interface IUnitOfWork : IDisposable
    {
        // Repository properties
        IRepository<T> Repository<T>() where T : class;
        
        // Specific repositories (if needed for complex operations)
        IMemberRepository Members { get; }
        IBookRepository Books { get; }
        INotificationRepository Notifications { get; }
        IRoomRepository Rooms { get; }
        IMemberVisitRepository MemberVisits { get; }
        ILibraryAssetRepository LibraryAssets { get; }
        
        // Transaction management
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

    // Specific repository interfaces for complex operations
    public interface IMemberRepository : IRepository<Entities.Member>
    {
        Task<Entities.Member?> GetByMembershipNumberAsync(string membershipNumber);
        Task<IEnumerable<Entities.Member>> GetMembersWithOverdueBooksAsync();
        Task<IEnumerable<Entities.Member>> GetExpiringMembershipsAsync(int daysAhead = 30);
    }

    public interface IBookRepository : IRepository<Entities.Book>
    {
        Task<IEnumerable<Entities.Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Entities.Book>> GetOverdueBooksAsync();
        Task<IEnumerable<Entities.Book>> SearchBooksAsync(string searchTerm);
    }

    public interface INotificationRepository : IRepository<Entities.Notification>
    {
        Task<IEnumerable<Entities.Notification>> GetUnreadNotificationsAsync(int memberId);
        Task<IEnumerable<Entities.Notification>> GetGlobalNotificationsAsync();
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int memberId);
    }

    public interface IRoomRepository : IRepository<Entities.Room>
    {
        Task<IEnumerable<Entities.Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeReservationId = null);
    }

    public interface IMemberVisitRepository : IRepository<Entities.MemberVisit>
    {
        Task<Entities.MemberVisit?> GetActiveVisitAsync(int memberId);
        Task<IEnumerable<Entities.MemberVisit>> GetVisitHistoryAsync(int memberId, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<Entities.MemberVisit>> GetCurrentVisitorsAsync();
    }

    public interface ILibraryAssetRepository : IRepository<Entities.LibraryAsset>
    {
        Task<IEnumerable<Entities.LibraryAsset>> GetAssetsByTypeAsync(Domain.Enums.AssetType type);
        Task<IEnumerable<Entities.LibraryAsset>> GetAssetsByStatusAsync(Domain.Enums.AssetStatus status);
        Task<IEnumerable<Entities.LibraryAsset>> GetAssetsNeedingMaintenanceAsync();
    }
}