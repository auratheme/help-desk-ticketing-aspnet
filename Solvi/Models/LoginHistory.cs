using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class LoginHistory
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(128)]
        public string? AspNetUserId { get; set; }
        public DateTime LoginDateTime { get; set; }
        [MaxLength(128)]
        public string? IsoUtcLoginDateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class LoginHistoryViewModel
    {
        public string? Id { get; set; }
        public string? AspNetUserId { get; set; }

        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        public string? Username { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
        public string? FullName { get; set; }
        [Display(Name = "LoginDateAndTime", ResourceType = typeof(Resources.Resource))]
        public DateTime LoginDateTime { get; set; }

        [Display(Name = "LoginDateAndTime", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcLoginDateTime { get; set; }
        [Display(Name = "Role", ResourceType = typeof(Resources.Resource))]
        public string? UserRole { get; set; }
    }
}