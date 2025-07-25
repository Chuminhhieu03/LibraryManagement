// Placeholder entities - these will be migrated from existing Models folder
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    // These entities exist in LibraryManagement/Models and will be migrated later
    public class Book : BaseAuditableEntity
    {
        public int BookId { get; set; }
        // ... other properties will be migrated
    }

    public class Author : BaseAuditableEntity
    {
        public int AuthorId { get; set; }
        // ... other properties will be migrated
    }

    public class BookLoan : BaseAuditableEntity
    {
        public int LoanId { get; set; }
        // ... other properties will be migrated
    }

    public class Fine : BaseAuditableEntity
    {
        public int FineId { get; set; }
        // ... other properties will be migrated
    }

    public class Reservation : BaseAuditableEntity
    {
        public int ReservationId { get; set; }
        // ... other properties will be migrated
    }

    public class Librarian : BaseAuditableEntity
    {
        public int LibrarianId { get; set; }
        // ... other properties will be migrated
    }
}