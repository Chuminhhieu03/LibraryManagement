namespace LibraryManagement.Domain.Common
{
    /// <summary>
    /// Represents the base entity class that serves as a foundation for other entities in the domain.
    /// </summary>
    /// <remarks>
    /// This abstract class includes common properties shared across multiple entities.
    /// </remarks>
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}