using Microsoft.AspNetCore.Mvc;
using Solvi.Data;
using Solvi.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solvi.ServiceInterfaces;

namespace Solvi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneralService _generalService;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext applicationDbContext, IGeneralService generalService, UserManager<IdentityUser> userManager)
        {
            db = applicationDbContext;
            _generalService = generalService;
            _userManager = userManager;

        }
        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }
        public IActionResult Index()
        {
            List<UserProfileViewModel> list = ReadUsers().ToList();
            return View(list);
        }

        public IQueryable<UserProfileViewModel> ReadUsers()
        {
            var result = from t1 in db.Users.AsNoTracking()
                         join t2 in db.UserProfiles.AsNoTracking() on t1.Id equals t2.AspNetUserId
                         join t3 in db.UserRoles.AsNoTracking() on t1.Id equals t3.UserId
                         where t2.IsDeleted == false
                         orderby t2.CreatedOn descending
                         select new UserProfileViewModel
                         {
                             Id = t2.Id,
                             AspNetUserId = t2.AspNetUserId,
                             Username = t1.UserName ?? "",
                             EmailAddress = t1.Email ?? "",
                             FullName = t2.FullName ?? "",
                             CreatedOn = t2.CreatedOn,
                             UserRoleName = db.Roles.Where(a => a.Id == t3.RoleId).Select(a => a.Name).FirstOrDefault(),
                             IsoUtcCreatedOn = t2.IsoUtcCreatedOn,
                             LatestLoginDateTime = db.LoginHistories.Where(a => a.AspNetUserId == t1.Id).OrderByDescending(a => a.LoginDateTime).Select(a => a.LoginDateTime).FirstOrDefault(),
                             LatestIsoUtcLoginDateTime = db.LoginHistories.Where(a => a.AspNetUserId == t1.Id).OrderByDescending(a => a.LoginDateTime).Select(a => a.IsoUtcLoginDateTime).FirstOrDefault()
                         };
            return result;
        }
    }
}
