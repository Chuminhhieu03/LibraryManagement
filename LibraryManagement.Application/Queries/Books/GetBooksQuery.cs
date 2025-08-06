using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Queries.Books
{
    /// Represents a query to retrieve a paginated list of books with optional filters.
    /// Implements the MediatR IRequest interface, returning an ApiResponse containing
    /// a PagedResponse of BookResponse objects.
    public class GetBooksQuery : IRequest<ApiResponse<PagedResponse<BookResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
    }
}