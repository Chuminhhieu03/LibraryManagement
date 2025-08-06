using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Shared.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling operations related to books.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;

        /// <summary>
        /// Controller responsible for handling operations related to books.
        /// </summary>
        public BooksController(IMediator mediator, ILogger<BooksController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="request">Book creation request</param>
        /// <returns>Created book details</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookResponse>>> CreateBook([FromBody] CreateBookRequest request)
        {
            var command = new CreateBookCommand(request);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}