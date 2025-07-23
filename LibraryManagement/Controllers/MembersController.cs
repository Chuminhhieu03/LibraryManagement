using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<MembersController> _logger;

        public MembersController(LibraryDbContext context, ILogger<MembersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            try
            {
                var members = await _context.Members.ToListAsync();
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching members");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            try
            {
                var member = await _context.Members
                    .Include(m => m.BookLoans)
                        .ThenInclude(bl => bl.Book)
                    .Include(m => m.Fines)
                    .Include(m => m.Reservations)
                        .ThenInclude(r => r.Book)
                    .FirstOrDefaultAsync(m => m.MemberId == id);

                if (member == null)
                {
                    return NotFound();
                }

                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching member with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/loans")]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetMemberLoans(int id)
        {
            try
            {
                var loans = await _context.BookLoans
                    .Include(bl => bl.Book)
                    .Include(bl => bl.ProcessedByLibrarian)
                    .Where(bl => bl.MemberId == id)
                    .ToListAsync();

                return Ok(loans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching loans for member {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/active-loans")]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetMemberActiveLoans(int id)
        {
            try
            {
                var activeLoans = await _context.BookLoans
                    .Include(bl => bl.Book)
                    .Where(bl => bl.MemberId == id && bl.Status == LoanStatus.Active)
                    .ToListAsync();

                return Ok(activeLoans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching active loans for member {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/fines")]
        public async Task<ActionResult<IEnumerable<Fine>>> GetMemberFines(int id)
        {
            try
            {
                var fines = await _context.Fines
                    .Include(f => f.BookLoan)
                        .ThenInclude(bl => bl!.Book)
                    .Where(f => f.MemberId == id)
                    .ToListAsync();

                return Ok(fines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching fines for member {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}