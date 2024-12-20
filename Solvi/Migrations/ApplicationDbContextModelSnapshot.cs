﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solvi.Data;

#nullable disable

namespace Solvi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "6aa449d9-ab7c-45bd-b1a6-b1ff0080159c",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "6cb3bb83-a665-4e17-ad10-63c722798710",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Solvi.Models.EmailLog", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Body")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FailedMessage")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("RecipientEmail")
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("Subject")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool?>("Success")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("EmailLog");
                });

            modelBuilder.Entity("Solvi.Models.EmailTemplate", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Body")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Type")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplate");

                    b.HasData(
                        new
                        {
                            Id = "0905c3b6-14da-4ef0-9bdb-78746b7274fa",
                            Body = "<p>Hi [FullName],<br><br>Thanks for signning up an account on [WebsiteName].</p><p>Click <a href=\"[Url]\">Here</a> to confirm your email in order to login. Thank You.</p><p>If you did not sign up an account on [WebsiteName], please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                            IsDeleted = false,
                            Subject = "Confirm Your Email To Complete [WebsiteName] Account Registration",
                            Type = "ConfirmEmail"
                        },
                        new
                        {
                            Id = "30abd78c-e709-40f2-80e3-4b5a1c58011f",
                            Body = "<p>Hi [FullName],<br><br>Kindly be informed that your password for the [WebsiteName] account has been reset by [ResetByName].</p><p>Below is your temporary new password to log in:<br><b>New Password:</b> [NewPassword]</p><p><b>NOTE:</b> As a safety precaution, you are advised to change your password after you log in later. Thank you.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                            IsDeleted = false,
                            Subject = "Password Reset For [WebsiteName] Account",
                            Type = "PasswordResetByAdmin"
                        },
                        new
                        {
                            Id = "8dfd1674-8b79-45dc-aaf8-a461d077c0f6",
                            Body = "<p>Hi [FullName],<br><br>There was a request to reset your password on [WebsiteName].</p><p><a href=\"[Url]\">Click Here</a> and follow the instructions to reset your password. Thank You.</p><p></p><p>If you did not make this request then please ignore this email.</p><p><i>Do not reply to this email.</i></p><p>Regards,<br>[WebsiteName]</p>",
                            IsDeleted = false,
                            Subject = "Password Reset For [WebsiteName] Account",
                            Type = "ForgotPassword"
                        },
                        new
                        {
                            Id = "19620959-560b-4ea1-9d8f-1d72148f1e3e",
                            Body = "Hi [FullName],<br><br>We hope this email finds you well.<br><br>We wanted to inform you that your support ticket with ID <b>[TicketCode]</b> has been updated. Our support team has diligently worked on your query, and we are pleased to inform you that a response is now available.<br><br>You can view the response and continue the conversation by logging into your account on our support portal or by following the link provided below:<br><a href=\"[TicketUrl]\">[TicketUrl]</a><br><br>Thank you for choosing [WebsiteName] for your support needs. We appreciate your patience and understanding.<br><br>Regards,<br>[WebsiteName]",
                            IsDeleted = false,
                            Subject = "[TicketCode]: Your Support Ticket Update",
                            Type = "NotifyCustomerOfTicketResponse"
                        },
                        new
                        {
                            Id = "0e1a3bb8-104c-44ca-b117-0fac72b100c2",
                            Body = "Hi [FullName],<br><br>A new ticket <b>[TicketCode]</b> has been submitted and is awaiting your action.<br><br>Ticket Details:<ul><li>Subject: [Subject]</li><li>Submitted By: [SubmittedBy]</li></ul>You may click <a href=\"[TicketUrl]\">here</a> to access the ticket and its details. Thank you for your attention to this matter.<br><br><i>Do not reply to this email.</i><br>Regards,<br>[WebsiteName]",
                            IsDeleted = false,
                            Subject = "[TicketCode]: New Ticket Submitted, Action Required",
                            Type = "NotifyAdminOnNewTicket"
                        });
                });

            modelBuilder.Entity("Solvi.Models.ErrorLog", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ErrorDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorDetails")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("ErrorLog");
                });

            modelBuilder.Entity("Solvi.Models.GlobalOptionSet", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Code")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Ordering")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("GlobalOptionSet");

                    b.HasData(
                        new
                        {
                            Id = "eb423f2e-781f-4ec7-b84b-0f31031bf01a",
                            Code = "ProfilePicture",
                            DisplayName = "Profile Picture",
                            IsDeleted = false,
                            Ordering = 1,
                            Type = "UserAttachment"
                        },
                        new
                        {
                            Id = "da95fd3e-e00b-40ce-a5a5-71dd0c316a2b",
                            Code = "Queueing",
                            DisplayName = "Queueing",
                            IsDeleted = false,
                            Ordering = 1,
                            Type = "TicketStatus"
                        },
                        new
                        {
                            Id = "2bcd4a22-8497-4009-9566-9fcf0ec570cc",
                            Code = "Open",
                            DisplayName = "Open",
                            IsDeleted = false,
                            Ordering = 2,
                            Type = "TicketStatus"
                        },
                        new
                        {
                            Id = "2b3631b6-4d7d-4f61-bda5-304fdc23b8eb",
                            Code = "Closed",
                            DisplayName = "Closed",
                            IsDeleted = false,
                            Ordering = 3,
                            Type = "TicketStatus"
                        });
                });

            modelBuilder.Entity("Solvi.Models.LoginHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("AspNetUserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcLoginDateTime")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LoginDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LoginHistory");
                });

            modelBuilder.Entity("Solvi.Models.SystemSetting", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogoFileName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("PortalName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("SmtpHost")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("SmtpPassword")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int?>("SmtpPort")
                        .HasColumnType("int");

                    b.Property<string>("SmtpUserName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("TimeZone")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("Id");

                    b.ToTable("SystemSetting");

                    b.HasData(
                        new
                        {
                            Id = "33319072-98e6-4c3d-aff6-388ccb30ad99",
                            IsDeleted = false,
                            PortalName = "Solvi",
                            SmtpHost = "",
                            SmtpPassword = "",
                            SmtpPort = 587,
                            SmtpUserName = "",
                            TimeZone = "Singapore Standard Time"
                        });
                });

            modelBuilder.Entity("Solvi.Models.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ClosedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ClosedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcClosedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcOpenedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("OpenedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("OpenedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Title")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("Solvi.Models.TicketReply", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Message")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("TicketId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketReply");
                });

            modelBuilder.Entity("Solvi.Models.UserAttachment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("AttachmentTypeId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("FileUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UniqueFileName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("Id");

                    b.ToTable("UserAttachment");
                });

            modelBuilder.Entity("Solvi.Models.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Address")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("AspNetUserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CountryName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("FullName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("IDCardNumber")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("IsoUtcCreatedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("IsoUtcModifiedOn")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UserStatusId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solvi.Models.TicketReply", b =>
                {
                    b.HasOne("Solvi.Models.Ticket", "Ticket")
                        .WithMany("TicketReplys")
                        .HasForeignKey("TicketId");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Solvi.Models.Ticket", b =>
                {
                    b.Navigation("TicketReplys");
                });
#pragma warning restore 612, 618
        }
    }
}
