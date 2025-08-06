namespace LibraryManagement.Shared.DTOs.Books
{
    /// <summary>
    /// Represents the response DTO/model for book details in the application.
    /// This class is used to encapsulate relevant book information that is sent as a part
    /// of API responses or query results.
    /// </summary>
    public class BookResponse
    {
        public int BookId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int Pages { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new();
        public decimal? Price { get; set; }
    }
}