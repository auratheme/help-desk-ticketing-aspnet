using Microsoft.AspNetCore.Mvc;
using Solvi.Data;
using Solvi.Models;
using Solvi.ServiceInterfaces;

namespace Solvi.Controllers
{
    public class LoginHistoryController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneralService _generalService;

        public LoginHistoryController(ApplicationDbContext applicationDbContext, IGeneralService generalService)
        {
            db = applicationDbContext;
            _generalService = generalService;

        }
        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }

        public IActionResult Index()
        {
            List<LoginHistoryViewModel> list = ReadRecords().ToList();
            return View(list);
        }

        public IQueryable<LoginHistoryViewModel> ReadRecords()
        {
            string userid = HttpContext.Items["UserId"] as string ?? "";
            bool isAdmin = User.IsInRole("Admin");
            var result = from t1 in db.LoginHistories
                         join t2 in db.Users on t1.AspNetUserId equals t2.Id
                         join t3 in db.UserProfiles on t1.AspNetUserId equals t3.AspNetUserId
                         join t4 in db.UserRoles on t1.AspNetUserId equals t4.UserId
                         join t5 in db.Roles on t4.RoleId equals t5.Id
                         where t1.IsDeleted == false && t3.IsDeleted == false
                         && (isAdmin || t1.AspNetUserId == userid)
                         orderby t1.LoginDateTime descending
                         select new LoginHistoryViewModel
                         {
                             Id = t1.Id ?? "",
                             AspNetUserId = t1.AspNetUserId ?? "",
                             Username = t2.UserName ?? "",
                             FullName = t3.FullName ?? "",
                             UserRole = t5.Name ?? "",
                             LoginDateTime = t1.LoginDateTime,
                             IsoUtcLoginDateTime = t1.IsoUtcLoginDateTime
                         };
            return result;
        }
    }
}
