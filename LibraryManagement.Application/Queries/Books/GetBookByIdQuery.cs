using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetBookByIdQuery : IRequest<ApiResponse<BookResponse>>
    {
        public int Id { get; set; }

        public GetBookByIdQuery(int id)
        {
            Id = id;
        }
    }
}