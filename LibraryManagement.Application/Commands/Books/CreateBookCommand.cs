using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Commands.Books
{
    /// Represents a command to create a new book in the library management system.
    /// This command is used in conjunction with MediatR to handle book creation requests.
    public class CreateBookCommand : IRequest<ApiResponse<BookResponse>>
    {
        public CreateBookRequest Request { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBookCommand"/> class.
        /// </summary>
        /// <param name="request">The request containing the book creation details.</param>
        public CreateBookCommand(CreateBookRequest request)
        {
            Request = request;
        }
    }
}