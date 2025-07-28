namespace LibraryManagement.Shared.DTOs.Books
{
    public class CreateBookRequest
    {
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int Pages { get; set; }
        public string? Language { get; set; }
        public int TotalCopies { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; } = new();
    }
}