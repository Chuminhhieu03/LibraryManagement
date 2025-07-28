using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetBooksQuery : IRequest<ApiResponse<PagedResponse<BookResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
    }
}