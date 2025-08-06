namespace LibraryManagement.Domain.Interfaces
{
    /// <summary>
    /// Represents a unit of work interface that serves as a single point for managing and coordinating
    /// transactions and repositories in a consistent manner.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region Repository properties

        /// <summary>
        /// Provides access to a repository for performing operations on a specified entity type.
        /// </summary>
        /// <typeparam name="T">The type of the entity for which the repository is created.</typeparam>
        /// <returns>An instance of IRepository for the specified entity type.</returns>
        IRepository<T> Repository<T>() where T : class;

        #endregion Repository properties

        #region Transaction management

        /// <summary>
        /// Asynchronously saves all changes made in the current unit of work to the underlying data source.
        /// Throws an exception if the operation fails, ensuring that no partial updates are persisted.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Asynchronously begins a database transaction, ensuring that subsequent operations
        /// are executed within the context of the same transaction. Throws an exception if
        /// a transaction is already in progress or if the operation fails.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Asynchronously commits the current transaction to the underlying data source.
        /// Ensures all operations within the transaction are finalized and persisted,
        /// or throws an exception if the transaction cannot be committed.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Asynchronously rolls back the currently active database transaction if one exists.
        /// Throws an InvalidOperationException if no transaction is in progress.
        /// After a rollback, the transaction is disposed and set to null.
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task RollbackTransactionAsync();

        #endregion Transaction management
    }
}