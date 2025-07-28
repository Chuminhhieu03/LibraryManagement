namespace LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository properties
        IRepository<T> Repository<T>() where T : class;
    
        // Transaction management   
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}