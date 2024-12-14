using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Solvi.ServiceInterfaces
{
    public interface IGeneralService
    {
        dynamic SetCreatedInfo(dynamic model, string? userid);
        dynamic SetModifiedInfo(dynamic model, string? userid);
        DateTime GetSystemTimeZoneDateTimeNow();
        string GetIsoUtcNow();
        string GetGlobalOptionSetId(string code, string type);
        string GetGlobalOptionSetDisplayNameByCode(string? code, string? type);
        void LogError(Exception? exception, string errorMessage);
        string? GetLogoUrl();
        string? GetPortalName();
    }

}
