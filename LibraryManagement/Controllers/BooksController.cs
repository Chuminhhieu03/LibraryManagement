using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(LibraryDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching books");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.Publisher)
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .FirstOrDefaultAsync(b => b.BookId == id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching book with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("publishers")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            try
            {
                var publishers = await _context.Publishers.ToListAsync();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching publishers");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}