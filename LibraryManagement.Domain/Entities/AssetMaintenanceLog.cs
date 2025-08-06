using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    /// <summary>
    /// Represents a log entry for the maintenance of a library asset.
    /// </summary>
    /// <remarks>
    /// This class is used to store information about the maintenance activities
    /// performed on a specific library asset, including details such as the type of maintenance,
    /// costs incurred, and the next scheduled maintenance date.
    /// </remarks>
    public class AssetMaintenanceLog : BaseEntity
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int AssetId { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(100)]
        public string MaintenanceType { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Cost { get; set; }

        [StringLength(100)]
        public string? TechnicianName { get; set; }

        [StringLength(100)]
        public string? CompanyName { get; set; }

        public DateTime? NextMaintenanceDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Navigation properties
        public virtual LibraryAsset Asset { get; set; } = null!;
    }
}