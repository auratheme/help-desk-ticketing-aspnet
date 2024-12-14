using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Solvi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Success = table.Column<bool>(type: "bit", nullable: true),
                    FailedMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ErrorDetails = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ErrorDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalOptionSet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Ordering = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalOptionSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AspNetUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsoUtcLoginDateTime = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSetting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LogoFileName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    PortalName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SmtpUserName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SmtpHost = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OpenedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OpenedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcOpenedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcClosedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAttachment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UniqueFileName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    AttachmentTypeId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AspNetUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IDCardNumber = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UserStatusId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketReply",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TicketId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcCreatedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsoUtcModifiedOn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReply_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6aa449d9-ab7c-45bd-b1a6-b1ff0080159c", null, "Admin", "ADMIN" },
                    { "6cb3bb83-a665-4e17-ad10-63c722798710", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "CreatedBy", "CreatedOn", "IsDeleted", "IsoUtcCreatedOn", "IsoUtcModifiedOn", "ModifiedBy", "ModifiedOn", "Subject", "Type" },
                values: new object[,]
                {
                    { "0905c3b6-14da-4ef0-9bdb-78746b7274fa", "<p>Hi [FullName],<br><br>Thanks for signning up an account on [WebsiteName].</p><p>Click <a href=\"[Url]\">Here</a> to confirm your email in order to login. Thank You.</p><p>If you did not sign up an account on [WebsiteName], please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>", null, null, false, null, null, null, null, "Confirm Your Email To Complete [WebsiteName] Account Registration", "ConfirmEmail" },
                    { "0e1a3bb8-104c-44ca-b117-0fac72b100c2", "Hi [FullName],<br><br>A new ticket <b>[TicketCode]</b> has been submitted and is awaiting your action.<br><br>Ticket Details:<ul><li>Subject: [Subject]</li><li>Submitted By: [SubmittedBy]</li></ul>You may click <a href=\"[TicketUrl]\">here</a> to access the ticket and its details. Thank you for your attention to this matter.<br><br><i>Do not reply to this email.</i><br>Regards,<br>[WebsiteName]", null, null, false, null, null, null, null, "[TicketCode]: New Ticket Submitted, Action Required", "NotifyAdminOnNewTicket" },
                    { "19620959-560b-4ea1-9d8f-1d72148f1e3e", "Hi [FullName],<br><br>We hope this email finds you well.<br><br>We wanted to inform you that your support ticket with ID <b>[TicketCode]</b> has been updated. Our support team has diligently worked on your query, and we are pleased to inform you that a response is now available.<br><br>You can view the response and continue the conversation by logging into your account on our support portal or by following the link provided below:<br><a href=\"[TicketUrl]\">[TicketUrl]</a><br><br>Thank you for choosing [WebsiteName] for your support needs. We appreciate your patience and understanding.<br><br>Regards,<br>[WebsiteName]", null, null, false, null, null, null, null, "[TicketCode]: Your Support Ticket Update", "NotifyCustomerOfTicketResponse" },
                    { "30abd78c-e709-40f2-80e3-4b5a1c58011f", "<p>Hi [FullName],<br><br>Kindly be informed that your password for the [WebsiteName] account has been reset by [ResetByName].</p><p>Below is your temporary new password to log in:<br><b>New Password:</b> [NewPassword]</p><p><b>NOTE:</b> As a safety precaution, you are advised to change your password after you log in later. Thank you.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>", null, null, false, null, null, null, null, "Password Reset For [WebsiteName] Account", "PasswordResetByAdmin" },
                    { "8dfd1674-8b79-45dc-aaf8-a461d077c0f6", "<p>Hi [FullName],<br><br>There was a request to reset your password on [WebsiteName].</p><p><a href=\"[Url]\">Click Here</a> and follow the instructions to reset your password. Thank You.</p><p></p><p>If you did not make this request then please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>", null, null, false, null, null, null, null, "Password Reset For [WebsiteName] Account", "ForgotPassword" }
                });

            migrationBuilder.InsertData(
                table: "GlobalOptionSet",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "DisplayName", "IsDeleted", "IsoUtcCreatedOn", "IsoUtcModifiedOn", "ModifiedBy", "ModifiedOn", "Ordering", "Type" },
                values: new object[,]
                {
                    { "2b3631b6-4d7d-4f61-bda5-304fdc23b8eb", "Closed", null, null, "Closed", false, null, null, null, null, 3, "TicketStatus" },
                    { "2bcd4a22-8497-4009-9566-9fcf0ec570cc", "Open", null, null, "Open", false, null, null, null, null, 2, "TicketStatus" },
                    { "da95fd3e-e00b-40ce-a5a5-71dd0c316a2b", "Queueing", null, null, "Queueing", false, null, null, null, null, 1, "TicketStatus" },
                    { "eb423f2e-781f-4ec7-b84b-0f31031bf01a", "ProfilePicture", null, null, "Profile Picture", false, null, null, null, null, 1, "UserAttachment" }
                });

            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "IsDeleted", "LogoFileName", "LogoUrl", "PortalName", "SmtpHost", "SmtpPassword", "SmtpPort", "SmtpUserName", "TimeZone" },
                values: new object[] { "33319072-98e6-4c3d-aff6-388ccb30ad99", false, null, null, "Solvi", "", "", 587, "", "Singapore Standard Time" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReply_TicketId",
                table: "TicketReply",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmailLog");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.DropTable(
                name: "GlobalOptionSet");

            migrationBuilder.DropTable(
                name: "LoginHistory");

            migrationBuilder.DropTable(
                name: "SystemSetting");

            migrationBuilder.DropTable(
                name: "TicketReply");

            migrationBuilder.DropTable(
                name: "UserAttachment");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}
