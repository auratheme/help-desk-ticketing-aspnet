using Microsoft.AspNetCore.Mvc;
using Solvi.Models;
using Solvi.ServiceInterfaces;
using System.Diagnostics;
using System.Globalization;

namespace Solvi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGeneralService _generalService;

        public HomeController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }

        public IActionResult Index()
        {
            try
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("index", "ticket");
                }
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            return View();
        }

        public IActionResult ChangeLanguage(string lang)
        {
            try
            {
                if (!string.IsNullOrEmpty(lang))
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    lang = "en";
                }
                Response.Cookies.Append("Language", lang);
                var referer = Request.GetTypedHeaders().Referer?.ToString();
                if (string.IsNullOrEmpty(referer))
                {
                    // Fallback to a default route if the referer is null or empty
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(referer);
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
