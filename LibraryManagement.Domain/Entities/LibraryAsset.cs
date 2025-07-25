using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class LibraryAsset : BaseAuditableEntity
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string AssetTag { get; set; } = string.Empty;

        public AssetType Type { get; set; }

        public AssetStatus Status { get; set; } = AssetStatus.Available;

        [Required]
        [StringLength(200)]
        public string PhysicalLocation { get; set; } = string.Empty;

        public int? ShelfId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Brand { get; set; }

        [StringLength(100)]
        public string? Model { get; set; }

        [StringLength(50)]
        public string? SerialNumber { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? WarrantyExpiry { get; set; }

        public DateTime? LastMaintenanceDate { get; set; }

        public DateTime? NextMaintenanceDate { get; set; }

        [StringLength(500)]
        public string? MaintenanceNotes { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Shelf? Shelf { get; set; }
        public virtual ICollection<AssetMaintenanceLog> MaintenanceLogs { get; set; } = new List<AssetMaintenanceLog>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}