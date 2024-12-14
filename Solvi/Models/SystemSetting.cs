using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class SystemSetting
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(4000)]
        public string? LogoFileName { get; set; }
        [MaxLength(4000)]
        public string? LogoUrl { get; set; }
        [MaxLength(4000)]
        public string? PortalName { get; set; }
        [MaxLength(4000)]
        public string? SmtpUserName { get; set; }
        [MaxLength(4000)]
        public string? SmtpPassword { get; set; }
        [MaxLength(4000)]
        public string? SmtpHost { get; set; }
        public int? SmtpPort { get; set; }
        [MaxLength(4000)]
        public string? TimeZone { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class SystemSettingViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(Resources.Resource))]
        public string? LogoFileName { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(Resources.Resource))]
        public IFormFile? Logo { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(Resources.Resource))]
        public string? LogoUrl { get; set; }

        [Required]
        [Display(Name = "PortalName", ResourceType = typeof(Resources.Resource))]
        public string PortalName { get; set; } = "";

        [Required]
        [Display(Name = "SMTPUsername", ResourceType = typeof(Resources.Resource))]
        public string SmtpUserName { get; set; } = "";

        [Required]
        [Display(Name = "SMTPPassword", ResourceType = typeof(Resources.Resource))]
        public string SmtpPassword { get; set; } = "";

        [Required]
        [Display(Name = "SMTPHost", ResourceType = typeof(Resources.Resource))]
        public string SmtpHost { get; set; } = "";

        [Required]
        [Display(Name = "SMTPPort", ResourceType = typeof(Resources.Resource))]
        public int? SmtpPort { get; set; }

        [Required]
        [Display(Name = "TimeZone", ResourceType = typeof(Resources.Resource))]
        public string TimeZone { get; set; } = "";

        [Required]
        [Display(Name = "QueueingStatusName", ResourceType = typeof(Resources.Resource))]
        public string QueueingDisplayName { get; set; } = "";

        [Required]
        [Display(Name = "OpenStatusName", ResourceType = typeof(Resources.Resource))]
        public string OpenDisplayName { get; set; } = "";

        [Required]
        [Display(Name = "ClosedStatusName", ResourceType = typeof(Resources.Resource))]
        public string ClosedDisplayName { get; set; } = "";

        public bool? DemoAccount { get; set; }

    }
}
