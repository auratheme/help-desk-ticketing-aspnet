using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class EmailTemplate
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(4000)]
        public string? Subject { get; set; }
        [MaxLength(4000)]
        public string? Body { get; set; }
        [MaxLength(512)]
        public string? Type { get; set; }
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