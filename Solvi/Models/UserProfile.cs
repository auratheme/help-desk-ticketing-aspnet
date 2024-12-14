using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
using Solvi.Helpers;

namespace Solvi.Models
{
    public class UserProfile
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(128)]
        public string? AspNetUserId { get; set; }
        [MaxLength(4000)]
        public string? FirstName { get; set; }
        [MaxLength(4000)]
        public string? LastName { get; set; }
        [MaxLength(4000)]
        public string? FullName { get; set; }
        [MaxLength(4000)]
        public string? IDCardNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(4000)]
        public string? CountryName { get; set; }
        [MaxLength(4000)]
        public string? Address { get; set; }
        [MaxLength(128)]
        public string? PostalCode { get; set; }
        [MaxLength(128)]
        public string? PhoneNumber { get; set; }
        [MaxLength(128)]
        public string? UserStatusId { get; set; }
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

    public class UserProfileViewModel
    {
        public string? Id { get; set; }
        public string? AspNetUserId { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        public string? FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
        public string? FullName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]\\w{3,29}$", ErrorMessage = "TheUsernameMustStartWithALetterAndCanOnly")]
        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        public string Username { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [PasswordValidation]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessage = "The field must match the password.")]
        public string ConfirmPassword { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
        public string EmailAddress { get; set; } = "";

        [Display(Name = "IDCardNumber", ResourceType = typeof(Resources.Resource))]
        public string? IDCardNumber { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resources.Resource))]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcDateOfBirth { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
        public string? CountryName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
        public List<SelectListItem> CountrySelectList { get; set; } = new List<SelectListItem>();

        [Display(Name = "FullAddress", ResourceType = typeof(Resources.Resource))]
        public string? Address { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(Resources.Resource))]
        public string? PostalCode { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resource))]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string? UserStatusId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string? UserStatusName { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public string? CreatedBy { get; set; }

        [Display(Name = "RegisteredOn", ResourceType = typeof(Resources.Resource))]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "RegisteredOn", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcCreatedOn { get; set; }

        [Display(Name = "ModifiedBy", ResourceType = typeof(Resources.Resource))]
        public string? ModifiedBy { get; set; }

        [Display(Name = "ModifiedOn", ResourceType = typeof(Resources.Resource))]
        public DateTime? ModifiedOn { get; set; }

        public string? IsoUtcModifiedOn { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Resources.Resource))]
        public string? UserRoleName { get; set; }

        [Display(Name = "ProfilePicture", ResourceType = typeof(Resources.Resource))]
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePictureFileName { get; set; }

        [Display(Name = "LatestLoginDateAndTime", ResourceType = typeof(Resources.Resource))]
        public DateTime? LatestLoginDateTime { get; set; }
        [Display(Name = "LatestLoginDateAndTime", ResourceType = typeof(Resources.Resource))]
        public string? LatestIsoUtcLoginDateTime { get; set; }
    }

    public class ImportFromExcel
    {
        [Display(Name = "File", ResourceType = typeof(Resources.Resource))]
        public IFormFile? File { get; set; }
        public List<ImportFromExcelError> ErrorList { get; set; } = new List<ImportFromExcelError>();
        public string? UploadResult { get; set; }
        public string? UserRole { get; set; }
        //The excel file name in wwwroot/exceltemplates folder
        public string? ExcelTemplateName { get; set; }
        public string? PageTitle { get; set; }
        public string? FormPostController { get; set; }
        public string? FormPostAction { get; set; }
        public string? DownloadTemplateUrl { get; set; }
        public ProjectEnum.TicketSetting? TicketSetting { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; } = new List<Breadcrumb>();
    }
}