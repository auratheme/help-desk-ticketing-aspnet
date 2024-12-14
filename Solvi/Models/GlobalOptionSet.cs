using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class GlobalOptionSet
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(512)]
        public string? Code { get; set; }
        [MaxLength(4000)]
        public string? DisplayName { get; set; }
        [MaxLength(512)]
        public string? Type { get; set; }
        public int? Ordering { get; set; }
        [MaxLength(128)]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        [MaxLength(128)]
        public string? IsoUtcCreatedOn { get; set; }
        [MaxLength(128)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [MaxLength(128)]
        public string? IsoUtcModifiedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}