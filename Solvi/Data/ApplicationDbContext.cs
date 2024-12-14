using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Solvi.Models;
using System.Globalization;

namespace Solvi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //add dbset here

        public DbSet<GlobalOptionSet> GlobalOptionSets { get; set; }
        public DbSet<UserAttachment> UserAttachments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketReply> TicketReplies { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set the default collation for the database to be case-insensitive (CI) and accent-sensitive (AS).
            // This ensures that string comparisons (such as searching and sorting) are case-insensitive by default.
            builder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            // Remove the convention that pluralizes table names. This prevents Entity Framework
            // from automatically naming tables in the plural form (e.g., "Users" instead of "User").
            builder.RemovePluralizingTableNameConvention();

            base.OnModelCreating(builder);

            // Configure the foreign key relationship between TicketReply and Ticket
            builder.Entity<TicketReply>()
                .HasOne(o => o.Ticket)
                .WithMany(c => c.TicketReplys)
                .HasForeignKey(o => o.TicketId);

            // Insert default data

            builder.Entity<SystemSetting>().HasData(
                new SystemSetting
                {
                    PortalName = "Solvi",
                    TimeZone = "Singapore Standard Time",
                    SmtpHost = "",
                    SmtpUserName = "",
                    SmtpPassword = "",
                    SmtpPort = 587
                }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
                );
            builder.Entity<GlobalOptionSet>().HasData(
                new GlobalOptionSet { Code = "ProfilePicture", DisplayName = "Profile Picture", Type = "UserAttachment", Ordering = 1 },
                new GlobalOptionSet { Code = "Queueing", DisplayName = "Queueing", Type = "TicketStatus", Ordering = 1 },
                new GlobalOptionSet { Code = "Open", DisplayName = "Open", Type = "TicketStatus", Ordering = 2 },
                new GlobalOptionSet { Code = "Closed", DisplayName = "Closed", Type = "TicketStatus", Ordering = 3 }
            );
            builder.Entity<EmailTemplate>().HasData(
                 new EmailTemplate
                 {
                     Subject = "Confirm Your Email To Complete [WebsiteName] Account Registration",
                     Body = "<p>Hi [FullName],<br><br>Thanks for signning up an account on [WebsiteName].</p><p>Click <a href=\"[Url]\">Here</a> to confirm your email in order to login. Thank You.</p><p>If you did not sign up an account on [WebsiteName], please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                     Type = "ConfirmEmail"
                 },
                 new EmailTemplate
                 {
                     Subject = "Password Reset For [WebsiteName] Account",
                     Body = "<p>Hi [FullName],<br><br>Kindly be informed that your password for the [WebsiteName] account has been reset by [ResetByName].</p><p>Below is your temporary new password to log in:<br><b>New Password:</b> [NewPassword]</p><p><b>NOTE:</b> As a safety precaution, you are advised to change your password after you log in later. Thank you.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                     Type = "PasswordResetByAdmin"
                 },
                 new EmailTemplate
                 {
                     Subject = "Password Reset For [WebsiteName] Account",
                     Body = "<p>Hi [FullName],<br><br>There was a request to reset your password on [WebsiteName].</p><p><a href=\"[Url]\">Click Here</a> and follow the instructions to reset your password. Thank You.</p><p></p><p>If you did not make this request then please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                     Type = "ForgotPassword"
                 },
                 new EmailTemplate
                 {
                     Subject = "[TicketCode]: Your Support Ticket Update",
                     Body = "Hi [FullName],<br><br>We hope this email finds you well.<br><br>We wanted to inform you that your support ticket with ID <b>[TicketCode]</b> has been updated. Our support team has diligently worked on your query, and we are pleased to inform you that a response is now available.<br><br>You can view the response and continue the conversation by logging into your account on our support portal or by following the link provided below:<br><a href=\"[TicketUrl]\">[TicketUrl]</a><br><br>Thank you for choosing [WebsiteName] for your support needs. We appreciate your patience and understanding.<br><br>Regards,<br>[WebsiteName]",
                     Type = "NotifyCustomerOfTicketResponse"
                 },
                 new EmailTemplate
                 {
                     Subject = "[TicketCode]: New Ticket Submitted, Action Required",
                     Body = "Hi [FullName],<br><br>A new ticket <b>[TicketCode]</b> has been submitted and is awaiting your action.<br><br>Ticket Details:<ul><li>Subject: [Subject]</li><li>Submitted By: [SubmittedBy]</li></ul>You may click <a href=\"[TicketUrl]\">here</a> to access the ticket and its details. Thank you for your attention to this matter.<br><br><i>Do not reply to this email.</i><br>Regards,<br>[WebsiteName]",
                     Type = "NotifyAdminOnNewTicket"
                 }
             );
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }
}
