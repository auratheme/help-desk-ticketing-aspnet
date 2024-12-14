using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class UserAttachment
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(4000)]
        public string? FileUrl { get; set; }
        [MaxLength(4000)]
        public string? FileName { get; set; }
        [MaxLength(4000)]
        public string? UniqueFileName { get; set; }
        [MaxLength(128)]
        public string? AttachmentTypeId { get; set; }
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