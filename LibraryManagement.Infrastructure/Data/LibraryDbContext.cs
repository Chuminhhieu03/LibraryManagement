using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // DbSets for existing entities
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
        public DbSet<MemberVisit> MemberVisits { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<LibraryAsset> LibraryAssets { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<AssetMaintenanceLog> AssetMaintenanceLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure existing relationships
            ConfigureBookRelationships(modelBuilder);
            ConfigureMemberRelationships(modelBuilder);
            ConfigureLibrarianRelationships(modelBuilder);

            // Configure NEW entity relationships - Thiếu các relationships này
            ConfigureNewEntityRelationships(modelBuilder);

            // Configure indexes and constraints
            ConfigureIndexesAndConstraints(modelBuilder);

            // Configure decimal precision
            ConfigureDecimalPrecision(modelBuilder);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void ConfigureBookRelationships(ModelBuilder modelBuilder)
        {
            // BookAuthor many-to-many relationship
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

            // Book relationships
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
        }

        private void ConfigureMemberRelationships(ModelBuilder modelBuilder)
        {
            // BookLoan relationships
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

            // Fine relationships
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

            // Reservation relationships
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
        }

        private void ConfigureLibrarianRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.ProcessedByLibrarian)
                .WithMany(l => l.BookLoansProcessed)
                .HasForeignKey(bl => bl.ProcessedByLibrarianId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.ProcessedByLibrarian)
                .WithMany(l => l.FinesProcessed)
                .HasForeignKey(f => f.ProcessedByLibrarianId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        // THIẾU PHẦN NÀY - Configure relationships cho các entity mới
        private void ConfigureNewEntityRelationships(ModelBuilder modelBuilder)
        {
            // MemberVisit relationships
            modelBuilder.Entity<MemberVisit>()
                .HasOne(mv => mv.Member)
                .WithMany(m => m.Visits)
                .HasForeignKey(mv => mv.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room & RoomReservation relationships
            modelBuilder.Entity<RoomReservation>()
                .HasOne(rr => rr.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(rr => rr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomReservation>()
                .HasOne(rr => rr.Member)
                .WithMany(m => m.RoomReservations)
                .HasForeignKey(rr => rr.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomReservation>()
                .HasOne(rr => rr.ApprovedByLibrarian)
                .WithMany()
                .HasForeignKey(rr => rr.ApprovedByLibrarianId)
                .OnDelete(DeleteBehavior.SetNull);

            // LibraryAsset & Shelf relationships
            modelBuilder.Entity<LibraryAsset>()
                .HasOne(la => la.Shelf)
                .WithMany(s => s.Assets)
                .HasForeignKey(la => la.ShelfId)
                .OnDelete(DeleteBehavior.SetNull);

            // AssetMaintenanceLog relationships
            modelBuilder.Entity<AssetMaintenanceLog>()
                .HasOne(aml => aml.Asset)
                .WithMany(la => la.MaintenanceLogs)
                .HasForeignKey(aml => aml.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Notification relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Member)
                .WithMany(m => m.Notifications)
                .HasForeignKey(n => n.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book & Shelf relationship (nếu Book có ShelfId)
            // modelBuilder.Entity<Book>()
            //     .HasOne(b => b.Shelf)
            //     .WithMany(s => s.Books)
            //     .HasForeignKey(b => b.ShelfId)
            //     .OnDelete(DeleteBehavior.SetNull);
        }

        private void ConfigureIndexesAndConstraints(ModelBuilder modelBuilder)
        {
            // Existing unique constraints
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

            // NEW indexes for new entities
            modelBuilder.Entity<LibraryAsset>()
                .HasIndex(la => la.AssetTag)
                .IsUnique();

            modelBuilder.Entity<Shelf>()
                .HasIndex(s => s.Code)
                .IsUnique();

            // Performance indexes
            modelBuilder.Entity<MemberVisit>()
                .HasIndex(mv => new { mv.MemberId, mv.CheckInTime });

            modelBuilder.Entity<MemberVisit>()
                .HasIndex(mv => mv.IsActive);

            modelBuilder.Entity<RoomReservation>()
                .HasIndex(rr => new { rr.RoomId, rr.StartTime, rr.EndTime });

            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.MemberId, n.IsRead, n.CreatedAt });
        }

        private void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
        {
            // Existing decimal configurations
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

            // NEW decimal configurations
            modelBuilder.Entity<LibraryAsset>()
                .Property(la => la.PurchasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RoomReservation>()
                .Property(rr => rr.TotalCost)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AssetMaintenanceLog>()
                .Property(aml => aml.Cost)
                .HasPrecision(10, 2);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Existing seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fiction", Description = "Fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 2, Name = "Non-Fiction", Description = "Non-fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 3, Name = "Science", Description = "Science and technology books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 4, Name = "History", Description = "History books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 5, Name = "Biography", Description = "Biographical books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 6, Name = "Children", Description = "Children's books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 7, Name = "Reference", Description = "Reference materials", CreatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, Name = "Penguin Random House", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 2, Name = "HarperCollins", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 3, Name = "Macmillan Publishers", Address = "London, UK", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 4, Name = "Simon & Schuster", Address = "New York, NY", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 5, Name = "Scholastic Corporation", Address = "New York, NY", CreatedAt = DateTime.UtcNow }
            );

            // NEW seed data for new entities
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    RoomId = 1,
                    Name = "Study Room A",
                    RoomNumber = "SR001",
                    Location = "Floor 1, Section A",
                    Type = Domain.Enums.RoomType.StudyRoom,
                    Capacity = 4,
                    CreatedAt = DateTime.UtcNow
                },
                new Room
                {
                    RoomId = 2,
                    Name = "Meeting Room B",
                    RoomNumber = "MR001",
                    Location = "Floor 2, Section B",
                    Type = Domain.Enums.RoomType.MeetingRoom,
                    Capacity = 8,
                    RequiresApproval = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Shelf>().HasData(
                new Shelf
                {
                    ShelfId = 1,
                    Code = "A001",
                    Location = "Floor 1, Section A",
                    Section = "Fiction",
                    Level = 1,
                    MaxCapacity = 100,
                    CreatedAt = DateTime.UtcNow
                },
                new Shelf
                {
                    ShelfId = 2,
                    Code = "B001",
                    Location = "Floor 1, Section B",
                    Section = "Science",
                    Level = 1,
                    MaxCapacity = 80,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}