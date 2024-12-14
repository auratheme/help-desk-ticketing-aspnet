using EmailTemplate = Solvi.Models.EmailTemplate;

namespace Solvi.ServiceInterfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string? toEmail, string? subject, string? body, string? userid);
        EmailTemplate? EmailTemplateForConfirmEmail(string? fullName, string? callbackUrl);
        EmailTemplate? EmailTemplateForForgotPassword(string? fullName, string? callbackUrl);
        Task<EmailTemplate?> EmailTemplateForNotifyCustomerOfTicketResponse(string? fullName, string? ticketCode, string? callbackUrl);
        Task<EmailTemplate?> EmailTemplateForNotifyAdminOnNewTicket(string? fullName, string? ticketCode, string? callbackUrl, string? ticketSubject, string? submittedBy);
    }
}
