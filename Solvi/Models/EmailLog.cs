using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class EmailLog
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(254)]
        public string? RecipientEmail { get; set; }
        [MaxLength(4000)]
        public string? Subject { get; set; }
        [MaxLength(4000)]
        public string? Body { get; set; }
        public bool? Success { get; set; }
        [MaxLength(4000)]
        public string? FailedMessage { get; set; }
        [MaxLength(128)]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        [MaxLength(128)]
        public string? IsoUtcCreatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class EmailLogViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "RecipientsEmail", ResourceType = typeof(Resources.Resource))]
        public string? RecipientEmail { get; set; }

        [Display(Name = "Subject", ResourceType = typeof(Resources.Resource))]
        public string? Subject { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resources.Resource))]
        public string? Body { get; set; }

        [Display(Name = "SentSuccessfully", ResourceType = typeof(Resources.Resource))]
        public bool? Success { get; set; }
        [Display(Name = "SentSuccessfully", ResourceType = typeof(Resources.Resource))]
        public string? SuccessYesNo { get; set; }

        public string? FailedMessage { get; set; }
        public string? CreatedBy { get; set; }
        [Display(Name = "CreatedOn", ResourceType = typeof(Resources.Resource))]
        public DateTime? CreatedOn { get; set; }
        [Display(Name = "CreatedOn", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcCreatedOn { get; set; }

        [Display(Name = "Action", ResourceType = typeof(Resources.Resource))]
        public string? Action { get; set; }
    }

}
