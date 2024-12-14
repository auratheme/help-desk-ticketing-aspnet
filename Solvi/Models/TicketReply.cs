using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class TicketReply
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(128)]
        public string? TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        [MaxLength(4000)]
        public string? Message { get; set; }
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

    public class TicketReplyViewModel
    {
        public string? Id { get; set; }

        public string? TicketId { get; set; }

        public string? Message { get; set; }
        
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? IsoUtcCreatedOn { get; set; }

        public string? SenderProfilePicture { get; set; }

        public string? SenderName { get; set; }

    }
}
