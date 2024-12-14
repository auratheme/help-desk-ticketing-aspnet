using System.Net.Mail;
using System.Net;
using Solvi.Models;
using Solvi.Data;
using EmailTemplate = Solvi.Models.EmailTemplate;
using Microsoft.EntityFrameworkCore;
using Solvi.ServiceInterfaces;
using Solvi.Helpers;

namespace Solvi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext db;
        private readonly IGeneralService _generalService;

        public EmailService(IConfiguration configuration, ApplicationDbContext applicationDbContext, IGeneralService generalService)
        {
            _configuration = configuration;
            db = applicationDbContext;
            _generalService = generalService;
        }

        public async Task<bool> SendEmailAsync(string? toEmail, string? subject, string? body, string? userid)
        {
            string error = "";
            bool success = false;
            try
            {
                if (!string.IsNullOrEmpty(toEmail) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(body))
                {
                    var smtp = await db.SystemSettings.Select(a => new { a.SmtpUserName, a.SmtpHost, a.SmtpPassword, a.SmtpPort }).FirstOrDefaultAsync();
                    string host = smtp?.SmtpHost ?? "";
                    int port = smtp?.SmtpPort ?? 587;
                    string userName = smtp?.SmtpUserName ?? "";
                    string password = smtp?.SmtpPassword ?? "";
                    if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                    {
                        var client = new SmtpClient(host, port);
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(userName, password);
                        client.EnableSsl = true;
                        MailAddress fromAddress = new MailAddress(userName, "Solvi");
                        MailMessage mail = new MailMessage(fromAddress, new MailAddress(toEmail));
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        client.Send(mail);
                        success = true;
                    }
                    else
                    {
                        error = "SMTP configuration is missing or incomplete. Please contact the system administrator to set up the required SMTP information.";
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex?.ToString() ?? "";
            }
            EmailLog emailLog = new EmailLog();
            emailLog.RecipientEmail = toEmail;
            emailLog.Subject = subject;
            emailLog.Body = body;
            emailLog.Success = success;
            emailLog.FailedMessage = error;
            _generalService.SetCreatedInfo(emailLog, userid);
            db.EmailLogs.Add(emailLog);
            db.SaveChanges();
            return success;
        }

        public EmailTemplate? EmailTemplateForConfirmEmail(string? fullName, string? callbackUrl)
        {
            string websiteName = db.SystemSettings.Select(a => a.PortalName).FirstOrDefault() ?? "";
            EmailTemplate? emailTemplate = db.EmailTemplates.AsNoTracking().Where(a => a.Type == ProjectEnum.EmailTemplate.ConfirmEmail.ToString()).FirstOrDefault();
            if (emailTemplate != null)
            {
                string subject = emailTemplate.Subject ?? "";
                string body = emailTemplate.Body ?? "";
                subject = GeneralHelper.ReplaceWords(subject, "[WebsiteName]", websiteName);
                body = GeneralHelper.ReplaceWords(body, "[FullName]", fullName ?? "Solvi user");
                body = GeneralHelper.ReplaceWords(body, "[WebsiteName]", websiteName);
                body = GeneralHelper.ReplaceWords(body, "[Url]", callbackUrl ?? "");
                emailTemplate.Subject = subject;
                emailTemplate.Body = body;
            }
            return emailTemplate;
        }

        public EmailTemplate? EmailTemplateForForgotPassword(string? fullName, string? callbackUrl)
        {
            string websiteName = db.SystemSettings.Select(a => a.PortalName).FirstOrDefault() ?? "";
            EmailTemplate? emailTemplate = db.EmailTemplates.AsNoTracking().Where(a => a.Type == ProjectEnum.EmailTemplate.ForgotPassword.ToString()).FirstOrDefault();
            if (emailTemplate != null)
            {
                string subject = emailTemplate.Subject ?? "";
                string body = emailTemplate.Body ?? "";
                subject = GeneralHelper.ReplaceWords(subject, "[WebsiteName]", websiteName);
                body = GeneralHelper.ReplaceWords(body, "[FullName]", fullName ?? "Solvi user");
                body = GeneralHelper.ReplaceWords(body, "[WebsiteName]", websiteName);
                body = GeneralHelper.ReplaceWords(body, "[Url]", callbackUrl ?? "");
                emailTemplate.Subject = subject;
                emailTemplate.Body = body;
            }
            return emailTemplate;
        }

        public async Task<EmailTemplate?> EmailTemplateForNotifyCustomerOfTicketResponse(string? fullName, string? ticketCode, string? callbackUrl)
        {
            string websiteName = await db.SystemSettings.Select(a => a.PortalName).FirstOrDefaultAsync() ?? "";
            EmailTemplate? emailTemplate = await db.EmailTemplates.AsNoTracking().Where(a => a.Type == ProjectEnum.EmailTemplate.NotifyCustomerOfTicketResponse.ToString()).FirstOrDefaultAsync();
            if (emailTemplate != null)
            {
                string subject = emailTemplate.Subject ?? "";
                string body = emailTemplate.Body ?? "";
                subject = GeneralHelper.ReplaceWords(subject, "[TicketCode]", ticketCode ?? "");
                body = GeneralHelper.ReplaceWords(body, "[FullName]", fullName ?? "Solvi user");
                body = GeneralHelper.ReplaceWords(body, "[TicketCode]", ticketCode ?? "");
                body = GeneralHelper.ReplaceWords(body, "[TicketUrl]", callbackUrl ?? "");
                body = GeneralHelper.ReplaceWords(body, "[WebsiteName]", websiteName);
                emailTemplate.Subject = subject;
                emailTemplate.Body = body;
            }
            return emailTemplate;
        }

        public async Task<EmailTemplate?> EmailTemplateForNotifyAdminOnNewTicket(string? fullName, string? ticketCode, string? callbackUrl, string? ticketSubject, string? submittedBy)
        {
            string websiteName = await db.SystemSettings.Select(a => a.PortalName).FirstOrDefaultAsync() ?? "";
            EmailTemplate? emailTemplate = await db.EmailTemplates.AsNoTracking().Where(a => a.Type == ProjectEnum.EmailTemplate.NotifyAdminOnNewTicket.ToString()).FirstOrDefaultAsync();
            if (emailTemplate != null)
            {
                string subject = emailTemplate.Subject ?? "";
                string body = emailTemplate.Body ?? "";
                subject = GeneralHelper.ReplaceWords(subject, "[TicketCode]", ticketCode ?? "");
                body = GeneralHelper.ReplaceWords(body, "[FullName]", fullName ?? "Solvi user");
                body = GeneralHelper.ReplaceWords(body, "[TicketCode]", ticketCode ?? "");
                body = GeneralHelper.ReplaceWords(body, "[Subject]", ticketSubject ?? "");
                body = GeneralHelper.ReplaceWords(body, "[SubmittedBy]", submittedBy ?? "");
                body = GeneralHelper.ReplaceWords(body, "[TicketUrl]", callbackUrl ?? "");
                body = GeneralHelper.ReplaceWords(body, "[WebsiteName]", websiteName);
                emailTemplate.Subject = subject;
                emailTemplate.Body = body;
            }
            return emailTemplate;
        }

    }
}
