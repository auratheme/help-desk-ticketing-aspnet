using Solvi.Models;
using Solvi.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Solvi.ServiceInterfaces;
using System.Security.Claims;

namespace Solvi.Services
{

    public class GeneralService : IGeneralService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GeneralService(IConfiguration configuration, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            db = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public dynamic SetCreatedInfo(dynamic model, string? userid)
        {
            model.CreatedBy = userid;
            model.CreatedOn = GetSystemTimeZoneDateTimeNow();
            model.IsoUtcCreatedOn = GetIsoUtcNow();
            return model;
        }

        public dynamic SetModifiedInfo(dynamic model, string? userid)
        {
            model.ModifiedBy = userid;
            model.ModifiedOn = GetSystemTimeZoneDateTimeNow();
            model.IsoUtcModifiedOn = GetIsoUtcNow();
            return model;
        }

        public DateTime GetSystemTimeZoneDateTimeNow()
        {
            string timeZone = db.SystemSettings.Select(a => a.TimeZone).FirstOrDefault() ?? "";
            if (!string.IsNullOrEmpty(timeZone))
            {
                DateTime dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, timeZone);
                return dateTime;
            }
            return DateTime.Now;
        }

        public string GetIsoUtcNow()
        {
            return DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
        }

        public string GetGlobalOptionSetId(string code, string type)
        {
            return db.GlobalOptionSets.Where(a => a.Code == code && a.Type == type).Select(a => a.Id).FirstOrDefault() ?? "";
        }

        public string GetGlobalOptionSetDisplayNameByCode(string? code, string? type)
        {
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(type))
            {
                string displayName = db.GlobalOptionSets.Where(a => a.Code == code && a.Type == type).Select(a => a.DisplayName).FirstOrDefault() ?? "";
                return displayName;
            }
            return "";
        }

        public void LogError(Exception? exception, string errorMessage)
        {
            var error = new ErrorLog
            {
                ErrorMessage = errorMessage,
                ErrorDetails = exception?.ToString() ?? "",
                ErrorDate = GetSystemTimeZoneDateTimeNow()
            };
            string userId = "";
            string email = "";
            var context = _httpContextAccessor.HttpContext;
            if (context != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                email = context.User.FindFirstValue(ClaimTypes.Email) ?? "";
            }
            error.UserId = userId;
            error.EmailAddress = email;
            db.ErrorLogs.Add(error);
            db.SaveChanges();
        }

        public string? GetLogoUrl()
        {
            string url = "";
            url = db.SystemSettings.Select(a => a.LogoUrl).FirstOrDefault() ?? "/assets/solvilogo.png";
            return url;
        }

        public string? GetPortalName()
        {
            string url = "";
            url = db.SystemSettings.Select(a => a.PortalName).FirstOrDefault() ?? "";
            return url;
        }

    }
}
