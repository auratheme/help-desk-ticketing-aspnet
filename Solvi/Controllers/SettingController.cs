using Solvi.Data;
using Solvi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solvi.Resources;
using Solvi.ServiceInterfaces;

namespace Solvi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneralService _generalService;
        private readonly IOptions<GeneralConfig> _generalConfig;

        public SettingController(ApplicationDbContext applicationDbContext, IGeneralService generalService, IOptions<GeneralConfig> generalConfig)
        {
            db = applicationDbContext;
            _generalService = generalService;
            _generalConfig = generalConfig;
        }

        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }

        public IActionResult Index()
        {
            SystemSettingViewModel model = new SystemSettingViewModel();
            model.DemoAccount = _generalConfig.Value.DemoAccount;
            if (_generalConfig.Value.DemoAccount == true)
            {
                return View(model);
            }
            SystemSetting? systemSetting = db.SystemSettings.FirstOrDefault();
            if (systemSetting != null)
            {
                model.Id = systemSetting.Id;
                model.LogoFileName = systemSetting.LogoFileName;
                model.LogoUrl = systemSetting.LogoUrl;
                model.PortalName = systemSetting.PortalName ?? "Solvi";
                model.SmtpUserName = systemSetting.SmtpUserName ?? "";
                model.SmtpPassword = systemSetting.SmtpPassword ?? "";
                model.SmtpHost = systemSetting.SmtpHost ?? "";
                model.SmtpPort = systemSetting.SmtpPort;
                model.TimeZone = systemSetting.TimeZone ?? "";
                var timezones = TimeZoneInfo.GetSystemTimeZones();
                ViewData["TimeZoneList"] = (from t1 in timezones
                                            select new SelectListItem
                                            {
                                                Text = t1.DisplayName,
                                                Value = t1.Id,
                                                Selected = t1.Id == systemSetting.TimeZone ? true : false
                                            }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SystemSettingViewModel model)
        {
            try
            {
                ModelState.Remove("QueueingDisplayName");
                ModelState.Remove("OpenDisplayName");
                ModelState.Remove("ClosedDisplayName");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                bool result = SaveRecord(model);
                if (result == false)
                {
                    TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                }
                else
                {
                    ModelState.Clear();
                    TempData["NotifySuccess"] = $"{Resource.SavedSuccessfully}!";
                }
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }

            return RedirectToAction("index");
        }

        public bool SaveRecord(SystemSettingViewModel model)
        {
            try
            {
                string userid = HttpContext.Items["UserId"] as string ?? "";
                SystemSetting? systemSetting = db.SystemSettings.FirstOrDefault();
                if (systemSetting != null)
                {
                    systemSetting.PortalName = model.PortalName;
                    systemSetting.SmtpUserName = model.SmtpUserName;
                    systemSetting.SmtpPassword = model.SmtpPassword;
                    systemSetting.SmtpHost = model.SmtpHost;
                    systemSetting.SmtpPort = model.SmtpPort;
                    systemSetting.TimeZone = model.TimeZone;
                    db.Entry(systemSetting).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
                return false;
            }
        }

        public IActionResult TicketStatus()
        {
            SystemSettingViewModel model = new SystemSettingViewModel();
            model.QueueingDisplayName = _generalService.GetGlobalOptionSetDisplayNameByCode(ProjectEnum.TicketStatus.Queueing.ToString(), "TicketStatus");
            model.OpenDisplayName = _generalService.GetGlobalOptionSetDisplayNameByCode(ProjectEnum.TicketStatus.Open.ToString(), "TicketStatus");
            model.ClosedDisplayName = _generalService.GetGlobalOptionSetDisplayNameByCode(ProjectEnum.TicketStatus.Closed.ToString(), "TicketStatus");
            return View(model);
        }

        [HttpPost]
        public IActionResult TicketStatus(SystemSettingViewModel model)
        {
            try
            {
                ModelState.Remove("Id");
                ModelState.Remove("LogoFileName");
                ModelState.Remove("Logo");
                ModelState.Remove("LogoUrl");
                ModelState.Remove("PortalName");
                ModelState.Remove("SmtpUserName");
                ModelState.Remove("SmtpPassword");
                ModelState.Remove("SmtpHost");
                ModelState.Remove("SmtpPort");
                ModelState.Remove("TimeZone");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                GlobalOptionSet? queueing = db.GlobalOptionSets.Where(a => a.Type == "TicketStatus" && a.Code == ProjectEnum.TicketStatus.Queueing.ToString()).FirstOrDefault();
                GlobalOptionSet? open = db.GlobalOptionSets.Where(a => a.Type == "TicketStatus" && a.Code == ProjectEnum.TicketStatus.Open.ToString()).FirstOrDefault();
                GlobalOptionSet? closed = db.GlobalOptionSets.Where(a => a.Type == "TicketStatus" && a.Code == ProjectEnum.TicketStatus.Closed.ToString()).FirstOrDefault();
                string userid = HttpContext.Items["UserId"] as string ?? "";
                if (queueing != null)
                {
                    queueing.DisplayName = model.QueueingDisplayName;
                    queueing.ModifiedBy = userid;
                    queueing.ModifiedOn = _generalService.GetSystemTimeZoneDateTimeNow();
                    queueing.IsoUtcModifiedOn = _generalService.GetIsoUtcNow();
                    db.Entry(queueing).State = EntityState.Modified;
                }
                if (open != null)
                {
                    open.DisplayName = model.OpenDisplayName;
                    open.ModifiedBy = userid;
                    open.ModifiedOn = _generalService.GetSystemTimeZoneDateTimeNow();
                    open.IsoUtcModifiedOn = _generalService.GetIsoUtcNow();
                    db.Entry(open).State = EntityState.Modified;
                }
                if (closed != null)
                {
                    closed.DisplayName = model.ClosedDisplayName;
                    closed.ModifiedBy = userid;
                    closed.ModifiedOn = _generalService.GetSystemTimeZoneDateTimeNow();
                    closed.IsoUtcModifiedOn = _generalService.GetIsoUtcNow();
                    db.Entry(closed).State = EntityState.Modified;
                }
                db.SaveChanges();
                ModelState.Clear();
                TempData["NotifySuccess"] = $"{Resource.SavedSuccessfully}!";
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }

            return RedirectToAction("ticketstatus");
        }
    }
}
