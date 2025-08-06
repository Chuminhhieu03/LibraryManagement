using MediatR;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Application.Queries.Books
{
    /// Represents a query to retrieve a book's details using its unique identifier.
    /// This query is processed by MediatR and returns an ApiResponse containing
    /// a BookResponse object.
    /// The result includes information about the book, such as its Title, ISBN,
    /// Description, PublicationDate, Pages, TotalCopies, AvailableCopies, Category,
    /// Publisher, Authors, and Price.
    /// Implements the MediatR IRequest interface to handle the query and response.
    public class GetBookByIdQuery : IRequest<ApiResponse<BookResponse>>
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBookByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the book to retrieve.</param>
        public GetBookByIdQuery(int id)
        {
            Id = id;
        }
    }
}