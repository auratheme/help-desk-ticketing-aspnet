using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Solvi.ServiceInterfaces;

namespace Solvi.Models
{
    public class UserProfilePictureActionFilter : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var generalService = (IGeneralService?)context.HttpContext.RequestServices.GetService(typeof(IGeneralService));
            if (generalService != null)
            {
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
                    {
                        ClaimsPrincipal currentUser = context.HttpContext.User;
                        if (currentUser != null)
                        {
                            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                            if (!string.IsNullOrEmpty(currentUserID))
                            {
                                // Retrieve the current user's roles
                                var roles = currentUser.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
                                controller.ViewBag.UserRoles = roles;
                            }
                        }
                    }
                    controller.ViewBag.LogoUrl = generalService.GetLogoUrl();
                    controller.ViewBag.PortalName = generalService.GetPortalName();
                }
            }
            await next();
        }
    }
}
