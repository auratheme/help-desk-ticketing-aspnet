using System.ComponentModel.DataAnnotations;
using Solvi.Helpers;

namespace Solvi.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; } = "";
        public bool? DemoAccount { get; set; }
        public bool RememberMe { get; set; } = false;
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        [StringLength(256, ErrorMessageResourceName = "TheFieldMustBeAtMost256CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression("^[A-Za-z]\\w{3,29}$", ErrorMessageResourceName = "TheUsernameMustStartWithALetterAndCanOnly", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; } = "";

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        [StringLength(256, ErrorMessageResourceName = "TheFieldMustBeAtMost256CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessageResourceName = "TheFieldMustBeAtMost256CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessageResourceName = "TheFieldMustBeAtMost100CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [PasswordValidation]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceName = "TheFieldMustMatchThePassword", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; } = "";
        public bool NoUserYet { get; set; }
        public string? TimezoneOffset { get; set; }
        public string? CountryCode { get; set; }
        public string? RegionCode { get; set; } 
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessageResourceName = "TheFieldMustBeAtMost100CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        [PasswordValidation]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceName = "TheFieldMustMatchThePassword", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; } = "";
        public string? Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; } = "";
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Resources.Resource))]
        public string OldPassword { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessageResourceName = "TheFieldMustBeAtMost100CharactersLong", ErrorMessageResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        [PasswordValidation]
        public string NewPassword { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("NewPassword", ErrorMessageResourceName = "TheFieldMustMatchTheNewPassword", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; } = "";
    }

}
