using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solvi.Data;
using Solvi.Resources;
using Solvi.Models;
using System.Data;
using Solvi.ServiceInterfaces;
using Solvi.Helpers;

namespace Solvi.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneralService _generalService;
        private IEmailService _emailService;

        public TicketController(ApplicationDbContext applicationDbContext, IGeneralService generalService, IEmailService emailService)
        {
            db = applicationDbContext;
            _generalService = generalService;
            _emailService = emailService;
        }

        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }

        [Authorize]
        public IActionResult DeleteTicket(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Ticket? ticket = db.Tickets.Find(id);
                if (ticket != null)
                {
                    string userid = HttpContext.Items["UserId"] as string ?? "";
                    ticket.IsDeleted = true;
                    _generalService.SetModifiedInfo(ticket, userid);
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["NotifySuccess"] = $"{Resource.Deleted}!";
                }
            }
            return RedirectToAction("index");
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Index()
        {
            List<TicketViewModel> list = ReadTickets().ToList();
            return View(list);
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Edit(string Id)
        {
            TicketViewModel model = new TicketViewModel();
            if (!string.IsNullOrEmpty(Id))
            {
                model.Id = Id;
                Ticket? ticket = db.Tickets.Find(Id);
                if (ticket != null)
                {
                    model.Title = ticket.Title ?? "";
                    model.Message = db.TicketReplies.Where(a => a.TicketId == Id).OrderBy(a => a.CreatedOn).Select(a => a.Message).FirstOrDefault() ?? "";
                }
            }
            return View(model);
        }

        public string GenerateTicketFullUrl(string ticketId)
        {
            string notificationUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/ticket/ticketreply/{ticketId}";
            return notificationUrl;
        }

        [Authorize]
        public IQueryable<TicketViewModel> ReadTickets()
        {
            string userid = HttpContext.Items["UserId"]?.ToString() ?? "";
            bool isAdmin = User.IsInRole("Admin");
            var result = from t1 in db.Tickets.AsNoTracking()
                         join t2 in db.GlobalOptionSets.AsNoTracking() on t1.Status equals t2.Code
                         let lastreplyby = db.TicketReplies.AsNoTracking().Where(a => a.TicketId == t1.Id).OrderByDescending(a => a.CreatedOn).Select(a => a.CreatedBy).FirstOrDefault()
                         let lastreplyon = db.TicketReplies.AsNoTracking().Where(a => a.TicketId == t1.Id).OrderByDescending(a => a.CreatedOn).Select(a => a.IsoUtcCreatedOn).FirstOrDefault()
                         where t1.IsDeleted == false && (t1.CreatedBy == userid || isAdmin) && t2.Type == "TicketStatus" && t2.IsDeleted == false
                         orderby t1.CreatedOn descending
                         select new TicketViewModel
                         {
                             Id = t1.Id,
                             Code = t1.Code,
                             Title = t1.Title ?? "",
                             StatusCode = t1.Status,
                             StatusName = t2.DisplayName,
                             CreatedBy = t1.CreatedBy,
                             CreatedByName = db.UserProfiles.Where(a => a.AspNetUserId == t1.CreatedBy).Select(a => a.FullName).FirstOrDefault(),
                             CreatedOn = t1.CreatedOn,
                             IsoUtcCreatedOn = t1.IsoUtcCreatedOn
                         };
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Edit(TicketViewModel model)
        {
            bool isreply = false;
            try
            {
                string userid = HttpContext.Items["UserId"] as string ?? "";
                if (!string.IsNullOrEmpty(model.Id))
                {
                    string createdBy = db.Tickets.Where(a => a.Id == model.Id).Select(a => a.CreatedBy).FirstOrDefault() ?? "";
                    if (createdBy != userid && !User.IsInRole("Admin"))
                    {
                        return View("_Unauthorized");
                    }
                }
                GeneralHelper.SanitizeModel(model);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Ticket? ticket = new Ticket();
                if (string.IsNullOrEmpty(model.Id))
                {
                    ticket.Title = model.Title;

                    int num = db.Tickets.Count() + 1;
                    string formattedNum = num.ToString("D6");
                    ticket.Code = $"Ticket-{formattedNum}";
                    ticket.IsClosed = false;
                    ticket.Status = ProjectEnum.TicketStatus.Queueing.ToString();
                    _generalService.SetCreatedInfo(ticket, userid);
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                    model.Id = ticket.Id;
                }
                else
                {
                    isreply = true;
                }

                string? ticketCreatorFullName = db.UserProfiles.Where(a => a.AspNetUserId == ticket.CreatedBy).Select(a => a.FullName).FirstOrDefault();

                TicketReply ticketReply = new TicketReply();
                ticketReply.TicketId = model.Id;
                ticketReply.Message = model.Message;
                _generalService.SetCreatedInfo(ticketReply, userid);
                db.TicketReplies.Add(ticketReply);
                db.SaveChanges();
                if (isreply)
                {
                    ticket = db.Tickets.Find(model.Id);
                    bool replied = (from t1 in db.TicketReplies
                                    join t2 in db.UserRoles on t1.CreatedBy equals t2.UserId
                                    join t3 in db.Roles on t2.RoleId equals t3.Id
                                    where t3.Name == "Admin"
                                    select t1.Id).Any();
                    //when admin replied, change the status from Queueing to Open
                    if (ticket != null && replied && ticket.Status == ProjectEnum.TicketStatus.Queueing.ToString())
                    {
                        ticket.Status = ProjectEnum.TicketStatus.Open.ToString();
                        ticket.OpenedBy = userid;
                        ticket.OpenedOn = _generalService.GetSystemTimeZoneDateTimeNow();
                        ticket.IsoUtcOpenedOn = _generalService.GetIsoUtcNow();
                        _generalService.SetModifiedInfo(ticket, userid);
                        db.Entry(ticket).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    string notificationUrl = GenerateTicketFullUrl(model.Id);
                    //when admin reply, send email to notify the customer
                    if (User.IsInRole("Admin") && ticket != null)
                    {
                        string? ticketByEmail = db.Users.Where(a => a.Id == ticket.CreatedBy).Select(a => a.Email).FirstOrDefault();
                        if (model.SendEmail)
                        {
                            EmailTemplate? emailTemplate = await _emailService.EmailTemplateForNotifyCustomerOfTicketResponse(ticketCreatorFullName, model.Code, notificationUrl);
                            await _emailService.SendEmailAsync(ticketByEmail, emailTemplate?.Subject, emailTemplate?.Body, userid);
                        }
                    }
                    TempData["NotifySuccess"] = $"{Resource.SavedSuccessfully}!";
                }
                else
                {
                    TempData["NotifySuccess"] = Resource.TicketSubmittedInQueueNow;
                    if (User.IsInRole("Customer"))
                    {
                        string notificationUrl = GenerateTicketFullUrl(model.Id);
                        var adminUsers = (from t1 in db.Users
                                          join t2 in db.UserRoles on t1.Id equals t2.UserId
                                          join t3 in db.Roles on t2.RoleId equals t3.Id
                                          where t3.Name == "Admin"
                                          select new { t1.Id, t1.Email }).ToList();
                        foreach (var adminUser in adminUsers)
                        {
                            string? fullname = db.UserProfiles.Where(a => a.AspNetUserId == adminUser.Id).Select(a => a.FullName).FirstOrDefault();
                            EmailTemplate? emailTemplate = await _emailService.EmailTemplateForNotifyAdminOnNewTicket(fullname, model.Code, notificationUrl, ticket.Title, ticketCreatorFullName);
                            await _emailService.SendEmailAsync(adminUser.Email, emailTemplate?.Subject, emailTemplate?.Body, userid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                LogCurrentControllerActionError(ex);
            }
            if (!isreply)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("ticketreply", new { Id = model.Id });
            }
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult TicketReply(string Id)
        {
            string userid = HttpContext.Items["UserId"] as string ?? "";
            TicketViewModel model = new TicketViewModel();
            if (!string.IsNullOrEmpty(Id))
            {
                Ticket? ticket = db.Tickets.Find(Id);
                if (ticket != null)
                {
                    model.Id = ticket.Id;
                    model.Title = ticket.Title ?? "";
                    model.StatusCode = ticket.Status;
                    model.StatusName = _generalService.GetGlobalOptionSetDisplayNameByCode(ticket.Status, "TicketStatus");
                    model.Code = ticket.Code;
                    string adminid = db.Roles.Where(a => a.Name == "Admin").Select(a => a.Id).FirstOrDefault() ?? "";
                    model.TicketReplies = (from t1 in db.TicketReplies.AsNoTracking()
                                           where t1.TicketId == ticket.Id
                                           select new TicketReplyViewModel
                                           {
                                               Id = t1.Id,
                                               Message = t1.Message,
                                               CreatedBy = t1.CreatedBy,
                                               CreatedOn = t1.CreatedOn,
                                               IsoUtcCreatedOn = t1.IsoUtcCreatedOn,
                                               SenderName = db.UserProfiles.Where(a => a.AspNetUserId == t1.CreatedBy).Select(a => a.FullName).FirstOrDefault(),
                                           }).OrderByDescending(a => a.CreatedOn).ToList();

                    model.OpenedBy = db.UserProfiles.Where(a => a.AspNetUserId == ticket.OpenedBy).Select(a => a.FullName).FirstOrDefault();
                    model.IsoUtcOpenedOn = ticket.IsoUtcOpenedOn;

                    model.IsClosed = ticket.IsClosed;
                    model.ClosedBy = db.UserProfiles.Where(a => a.AspNetUserId == ticket.ClosedBy).Select(a => a.FullName).FirstOrDefault();
                    model.IsoUtcClosedOn = ticket.IsoUtcClosedOn;
                    model.CanAccess = userid == ticket.CreatedBy || User.IsInRole("Admin");

                    foreach (var item in model.TicketReplies)
                    {
                        item.Message = item.Message?.Replace("&lt;a href", "&lt;a class=\"color-primarycolor fw600\" href");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CloseTicket(string Id)
        {
            try
            {
                string userid = HttpContext.Items["UserId"] as string ?? "";
                if (!string.IsNullOrEmpty(Id))
                {
                    string? createdBy = db.Tickets.Where(a => a.Id == Id).Select(a => a.CreatedBy).FirstOrDefault();
                    if (createdBy != userid && !User.IsInRole("Admin"))
                    {
                        return View("_Unauthorized");
                    }
                    Ticket? ticket = db.Tickets.Find(Id);
                    if (ticket != null)
                    {
                        ticket.IsClosed = true;
                        ticket.Status = ProjectEnum.TicketStatus.Closed.ToString();
                        ticket.ClosedBy = userid;
                        ticket.ClosedOn = _generalService.GetSystemTimeZoneDateTimeNow();
                        ticket.IsoUtcClosedOn = _generalService.GetIsoUtcNow();
                        db.Entry(ticket).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        TempData["NotifySuccess"] = Resource.HurrayTheTicketWasClosedThankYou;
                    }
                    else
                    {
                        TempData["NotifyError"] = "Ticket not found.";
                    }
                }
                else
                {
                    return View("_PageNotFound");
                }
            }
            catch (Exception ex)
            {
                TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                LogCurrentControllerActionError(ex);
            }
            return RedirectToAction("ticketreply", "ticket", new { id = Id });
        }

    }
}