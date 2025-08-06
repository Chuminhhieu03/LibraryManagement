namespace LibraryManagement.Shared.DTOs.Common
{
    /// <summary>
    /// Represents a paginated response containing a collection of items and pagination metadata.
    /// </summary>
    /// <typeparam name="T">The type of the items in the paginated response.</typeparam>
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Creates and returns a new instance of PagedResponse with the specified items, page number, page size, and total records.
        /// </summary>
        /// <typeparam name="T">The type of the items in the response.</typeparam>
        /// <param name="items">The collection of items for the current page.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="totalRecords">The total number of records across all pages.</param>
        /// <returns>A newly created instance of PagedResponse containing the provided data and computed pagination details.</returns>
        public static PagedResponse<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalRecords)
        {
            return new PagedResponse<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };
        }
    }
}