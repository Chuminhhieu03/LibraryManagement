using System.Linq.Expressions;

namespace LibraryManagement.Domain.Interfaces
{
    /// <summary>
    /// Represents the generic repository interface for providing CRUD operations
    /// and querying methods for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The entity type the repository operates on, constrained to reference types.</typeparam>
    public interface IRepository<T> where T : class
    {
        #region Read operations

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves an entity of type T by its identifier, including related entities.
        /// </summary>
        /// <param name="id">The identifier of the entity to be retrieved.</param>
        /// <param name="includes">An array of expressions specifying related entities to include in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity of type T if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves all entities, including related entities if specified.
        /// </summary>
        /// <param name="includes">An array of expressions specifying related entities to include in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Asynchronously retrieves a collection of entities that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">A LINQ expression specifying the conditions to filter entities.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of entities that match the specified predicate.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves a collection of entities that match the specified predicate.
        /// </summary>
        /// <param name="predicate">A lambda expression representing the filter criteria for the entities to retrieve.</param>
        /// <param name="includes">An array of expressions specifying related entities to include in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of entities that match the specified criteria.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate or returns null if none is found.
        /// </summary>
        /// <param name="predicate">The condition to filter the entity.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the first entity that matches the predicate, if found; otherwise, null.</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate or null if no match is found.
        /// </summary>
        /// <param name="predicate">A lambda expression specifying the condition the entity must satisfy.</param>
        /// <param name="includes">An array of expressions specifying related entities to include in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the first entity that matches the predicate, or null if no match is found.</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        #endregion Read operations

        #region Paging

        /// <summary>
        /// Asynchronously retrieves a paginated list of entities based on specified parameters.
        /// </summary>
        /// <param name="pageIndex">The index of the page to retrieve, starting from 0.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="filter">An optional filter expression to apply to the entities.</param>
        /// <param name="orderBy">An optional function to order the resulting entities.</param>
        /// <param name="includes">An optional array of navigation properties to include in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a tuple where the first item is the collection of entities for the current page, and the second item is the total count of entities satisfying the given criteria.</returns>
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes);

        #endregion Paging

        #region Aggregation

        /// <summary>
        /// Asynchronously retrieves the total number of entities in the repository.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the total count of entities.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        /// Asynchronously counts the entities that satisfy the provided condition.
        /// </summary>
        /// <param name="predicate">A lambda expression to define the condition for filtering the entities.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of entities satisfying the condition.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously determines whether any entity satisfies the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to test each entity against.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if any entity satisfies the predicate; otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        #endregion Aggregation

        #region Write operations

        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added to the repository.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Asynchronously adds a collection of entities to the database.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added entities.</returns>
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Asynchronously updates an existing entity in the data source.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously updates a range of entities in the data source.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Asynchronously deletes the specified entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Asynchronously deletes a range of entities from the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteRangeAsync(IEnumerable<T> entities);

        #endregion Write operations
    }
}