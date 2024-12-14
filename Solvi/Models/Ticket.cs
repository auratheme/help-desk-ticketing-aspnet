using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class Ticket
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(4000)]
        public string? Title { get; set; }
        [MaxLength(4000)]
        public string? Status { get; set; }
        [MaxLength(4000)]
        public string? Code { get; set; }
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
        [MaxLength(128)]
        public string? OpenedBy { get; set; }
        public DateTime? OpenedOn { get; set; }
        [MaxLength(128)]
        public string? IsoUtcOpenedOn { get; set; }
        public bool? IsClosed { get; set; }
        [MaxLength(128)]
        public string? ClosedBy { get; set; }
        public DateTime? ClosedOn { get; set; }
        [MaxLength(128)]
        public string? IsoUtcClosedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<TicketReply>? TicketReplys { get; set; }
    }

    public class TicketViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Message { get; set; } = "";

        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string? StatusCode { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string? StatusName { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resource))]
        public string? CreatedBy { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resource))]
        public string? CreatedByName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? IsoUtcCreatedOn { get; set; }

        public string? Code { get; set; }

        public List<TicketReplyViewModel> TicketReplies { get; set; } = new List<TicketReplyViewModel>();

        public bool SendEmail { get; set; } = true;

        public bool? IsClosed { get; set; }

        [Display(Name = "ClosedBy", ResourceType = typeof(Resources.Resource))]
        public string? ClosedBy { get; set; }

        [Display(Name = "ClosedOn", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcClosedOn { get; set; }

        public string? OpenedBy { get; set; }

        public string? IsoUtcOpenedOn { get; set; }

        public bool CanAccess { get; set; }
    }
}
