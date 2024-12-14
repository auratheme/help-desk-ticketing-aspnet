using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class ErrorLog
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(128)]
        public string? UserId { get; set; }
        [MaxLength(128)]
        public string? EmailAddress { get; set; }
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        [MaxLength(4000)]
        public string? ErrorDetails { get; set; }
        public DateTime? ErrorDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class ErrorLogViewModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
        public string? EmailAddress { get; set; }

        [Display(Name = "ErrorMessage", ResourceType = typeof(Resources.Resource))]
        public string? ErrorMessage { get; set; }

        [Display(Name = "ErrorDetail", ResourceType = typeof(Resources.Resource))]
        public string? ErrorDetails { get; set; }

        [Display(Name = "ErrorDate", ResourceType = typeof(Resources.Resource))]
        public DateTime? ErrorDate { get; set; }
    }


}
