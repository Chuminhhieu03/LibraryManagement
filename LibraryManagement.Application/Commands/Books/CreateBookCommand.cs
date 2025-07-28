using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<ApiResponse<BookResponse>>
    {
        public CreateBookRequest Request { get; set; }

        public CreateBookCommand(CreateBookRequest request)
        {
            Request = request;
        }
    }
}