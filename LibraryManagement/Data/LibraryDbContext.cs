using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BookAuthor many-to-many relationship
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => ba.BookAuthorId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Book relationships
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure BookLoan relationships
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Member)
                .WithMany(m => m.BookLoans)
                .HasForeignKey(bl => bl.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.ProcessedByLibrarian)
                .WithMany(l => l.BookLoansProcessed)
                .HasForeignKey(bl => bl.ProcessedByLibrarianId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Fine relationships
            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Member)
                .WithMany(m => m.Fines)
                .HasForeignKey(f => f.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.BookLoan)
                .WithMany(bl => bl.Fines)
                .HasForeignKey(f => f.BookLoanId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.ProcessedByLibrarian)
                .WithMany(l => l.FinesProcessed)
                .HasForeignKey(f => f.ProcessedByLibrarianId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Reservation relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reservations)
                .HasForeignKey(r => r.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.MembershipNumber)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Librarian>()
                .HasIndex(l => l.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<Librarian>()
                .HasIndex(l => l.Email)
                .IsUnique();

            modelBuilder.Entity<Author>()
                .HasIndex(a => new { a.FirstName, a.LastName });

            modelBuilder.Entity<BookAuthor>()
                .HasIndex(ba => new { ba.BookId, ba.AuthorId })
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Member>()
                .Property(m => m.TotalFines)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Librarian>()
                .Property(l => l.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BookLoan>()
                .Property(bl => bl.FineAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Fine>()
                .Property(f => f.Amount)
                .HasPrecision(10, 2);

            // Seed some initial data (optional)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fiction", Description = "Fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 2, Name = "Non-Fiction", Description = "Non-fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 3, Name = "Science", Description = "Science and technology books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 4, Name = "History", Description = "History books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 5, Name = "Biography", Description = "Biographical books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 6, Name = "Children", Description = "Children's books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 7, Name = "Reference", Description = "Reference materials", CreatedAt = DateTime.UtcNow }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, Name = "Penguin Random House", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 2, Name = "HarperCollins", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 3, Name = "Macmillan Publishers", Address = "London, UK", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 4, Name = "Simon & Schuster", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 5, Name = "Scholastic Corporation", Address = "New York, NY", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}