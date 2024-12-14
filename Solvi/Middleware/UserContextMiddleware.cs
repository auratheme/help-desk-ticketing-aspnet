using Microsoft.AspNetCore.Identity;

namespace Solvi.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<IdentityUser> userManager)
        {
            if (context != null)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user != null)
                {
                    context.Items["UserId"] = user.Id;
                    context.Items["UserEmail"] = user.Email;
                }
                await _next(context);
            }
        }
    }
}
