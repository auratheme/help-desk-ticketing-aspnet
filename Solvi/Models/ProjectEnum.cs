namespace Solvi.Models
{
    //project default enum

    public class ProjectEnum
    {
        public enum UserAttachment
        {
            ProfilePicture
        }

        public enum UserStatus
        {
            Registered,
            Validated,
            NotValidated,
            Banned
        }

        public enum EmailTemplate
        {
            ForgotPassword,
            ConfirmEmail,
            PasswordResetByAdmin,
            TicketAssigned,
            NotifyCustomerOfTicketResponse,
            NotifyAdminOnNewTicket
        }

        public enum TicketStatus
        {
            Queueing,
            Open,
            Closed
        }

        public enum MessageType
        {
            ConfirmEmailSent,
            SuccessfullyConfirmedEmail,
            FailedToConfirmEmail,
            ForgotPasswordEmailSent
        }

        public enum TicketSetting
        {
            RequestType,
            Impact,
            TicketCategory,
            Urgency,
            Priority,
            Empty
        }
    }

}