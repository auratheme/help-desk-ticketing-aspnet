using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solvi.Data;
using Solvi.Helpers;
using Solvi.Models;
using Solvi.Resources;
using Solvi.ServiceInterfaces;
using System.Data;

namespace Solvi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext db;
        private readonly IEmailService _emailService;
        private readonly IOptions<GeneralConfig> _generalConfig;
        private readonly IGeneralService _generalService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            ApplicationDbContext applicationDbContext, IEmailService emailService, IOptions<GeneralConfig> generalConfig, IGeneralService generalService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = applicationDbContext;
            _emailService = emailService;
            _generalConfig = generalConfig;
            _generalService = generalService;
        }

        public void LogCurrentControllerActionError(Exception ex)
        {
            _generalService.LogError(ex, $"{GetType().Name} - {ControllerContext.RouteData.Values["action"]?.ToString()} Method");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                DemoAccount = _generalConfig.Value.DemoAccount
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string userid = "";
                bool confirmedEmail = false;
                // if username input contains @ sign, means that user use email to login
                IdentityUser? user = model.UserName.Contains('@') ? await _userManager.FindByEmailAsync(model.UserName) :
                        await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    model.UserName = user.UserName ?? "";
                    userid = user.Id;
                    confirmedEmail = user.EmailConfirmed;
                    if (_generalConfig.Value.ConfirmEmailToLogin)
                    {
                        if (!confirmedEmail)
                        {
                            TempData["NotifyFailed"] = Resource.PleaseConfirmYourEmailBeforeSigningIn;
                            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            string? callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);
                            string fullName = db.UserProfiles.Where(a => a.AspNetUserId == user.Id).Select(a => a.FullName).FirstOrDefault() ?? "";
                            EmailTemplate? emailTemplate = _emailService.EmailTemplateForConfirmEmail(fullName, callbackUrl ?? "");
                            await _emailService.SendEmailAsync(user.Email ?? "", emailTemplate?.Subject ?? "", emailTemplate?.Body ?? "", user.Id);
                            return RedirectToAction("login");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Resource.InvalidLoginAttempt);
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    SaveLoginHistory(userid);
                    return RedirectToAction("index", "ticket");
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                ModelState.AddModelError("", Resource.InvalidLoginAttempt);
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (_generalConfig.Value.DemoAccount == true && User.Identity?.Name == "admin")
                {
                    TempData["NotifyFailed"] = "Sorry, please do not change the password for the demo account. You may register a new account in order to try the password change feature.";
                    return RedirectToAction("index", "ticket");
                }
                var user = await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
                if (user == null)
                {
                    // Handle the case when the user is not found.
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model); // or return an appropriate response
                }
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    TempData["NotifySuccess"] = Resource.PasswordChangedSuccessfully;
                    return RedirectToAction("index", "ticket");
                }
                AddErrors(result);
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }

            return View(model);
        }

        public async Task<IActionResult> MyProfile(string? id)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            if (!string.IsNullOrEmpty(id) && User.IsInRole("Admin"))
            {
                model = await GetCurrentUserProfile(id);
            }
            else
            {
                string userid = _userManager.GetUserId(User) ?? "";
                string upId = db.UserProfiles.Where(a => a.AspNetUserId == userid).Select(a => a.Id).FirstOrDefault() ?? "";
                model = await GetCurrentUserProfile(upId);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult MyProfile(UserProfileViewModel model)
        {
            try
            {
                GeneralHelper.SanitizeModel(model);
                string userid = HttpContext.Items["UserId"] as string ?? "";
                ModelState.Remove("Password");
                //ModelState.Remove("Username");
                //ModelState.Remove("EmailAddress");
                ModelState.Remove("ProfilePicture");
                if (ModelState.IsValid)
                {

                    UserProfile? userProfile = db.UserProfiles.Find(model.Id);
                    if (userProfile != null)
                    {
                        userProfile.FullName = model.FullName;
                        userProfile.FirstName = model.FirstName;
                        userProfile.LastName = model.LastName;
                        userProfile.PhoneNumber = model.PhoneNumber;
                        userProfile.CountryName = model.CountryName;
                        userProfile.PostalCode = model.PostalCode;
                        userProfile.Address = model.Address;
                        db.Entry(userProfile).State = EntityState.Modified;

                        IdentityUser? user = db.Users.Find(userProfile.AspNetUserId);
                        if (user != null)
                        {
                            user.UserName = model.Username;
                            user.Email = model.EmailAddress;
                            db.Entry(user).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        TempData["NotifySuccess"] = Resource.ChangesWereSavedSuccessfully;
                    }
                    else
                    {
                        TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                    }
                }
                else
                {
                    return View("myprofile", model);
                }
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            return RedirectToAction("myprofile");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel model = new()
            {
                NoUserYet = await HaveAnyUserInSystem() == false
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                //if don't have any user yet in the system, this is the first registered user, assign Admin to this user
                //assumming the first user who access the system is the Admin
                bool haveUsersInSystem = await HaveAnyUserInSystem();
                string role = !haveUsersInSystem ? "Admin" : "Customer";

                await RegisterUser(model, _generalConfig.Value.ConfirmEmailToLogin, role);
                if (ModelState.IsValid)
                {
                    if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                    {
                        TempData["NotifySuccess"] = Resource.RegisterSuccess;
                        return RedirectToAction("index", "user");
                    }
                    else
                    {
                        TempData["NotifySuccess"] = _generalConfig.Value.ConfirmEmailToLogin ? Resource.AnEmailWasSentToYourRegisteredEmailAddress : Resource.RegisterSuccessLoginNow;
                        if (_generalConfig.Value.ConfirmEmailToLogin)
                        {
                            ViewBag.MessageType = ProjectEnum.MessageType.ConfirmEmailSent.ToString();
                            return View("Message", "account");
                        }
                        else
                        {
                            return RedirectToAction("login", "account");
                        }
                    }
                }
                model.NoUserYet = haveUsersInSystem == false;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                LogCurrentControllerActionError(ex);
                return RedirectToAction("register");
            }
        }

        [AllowAnonymous]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            try
            {
                await RegisterUser(model, _generalConfig.Value.ConfirmEmailToLogin, "Admin");
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                    {
                        TempData["NotifySuccess"] = Resource.AnAdminAccountWasAddedSuccessfully;
                        return RedirectToAction("index", "user");
                    }
                    else
                    {
                        TempData["NotifySuccess"] = Resource.RegisterSuccessLoginNow;
                        return RedirectToAction("login", "account");
                    }
                }
                model.NoUserYet = await HaveAnyUserInSystem() == false;
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                LogCurrentControllerActionError(ex);
                return RedirectToAction("registeradmin");
            }
        }

        public IActionResult Message()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return View("Error");
            }
            var user = db.Users.Find(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    ViewBag.MessageType = ProjectEnum.MessageType.SuccessfullyConfirmedEmail.ToString();
                    return View("Message");
                }
                else
                {
                    string newCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string? callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = newCode }, protocol: Request.Scheme);
                    string fullName = db.UserProfiles.Where(a => a.AspNetUserId == user.Id).Select(a => a.FullName).FirstOrDefault() ?? "";
                    EmailTemplate? emailTemplate = _emailService.EmailTemplateForConfirmEmail(fullName, callbackUrl ?? "");
                    await _emailService.SendEmailAsync(user.Email ?? "", emailTemplate?.Subject ?? "", emailTemplate?.Body ?? "", user.Id);
                    ViewBag.MessageType = ProjectEnum.MessageType.FailedToConfirmEmail.ToString();
                    return View("Message");
                }
            }
            return View("Error");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await GetUserByEmail(model.Email);
                    if (user == null)
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        ModelState.Clear();
                        TempData["NotifySuccess"] = Resource.ResetPasswordEmailSent;
                        return RedirectToAction("login", "account");
                    }

                    // Send an email with this link
                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);
                    string fullName = db.UserProfiles.Where(a => a.AspNetUserId == user.Id).Select(a => a.FullName).FirstOrDefault() ?? "";
                    EmailTemplate? emailTemplate = _emailService.EmailTemplateForForgotPassword(fullName, callbackUrl ?? "");
                    await _emailService.SendEmailAsync(user.Email ?? "", emailTemplate?.Subject ?? "", emailTemplate?.Body ?? "", user.Id);

                    ModelState.Clear();
                    ViewBag.MessageType = ProjectEnum.MessageType.ForgotPasswordEmailSent.ToString();
                    return View("Message", "account");
                }
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string code)
        {
            var email = db.Users.Where(a => a.Id == userId).Select(a => a.Email).FirstOrDefault() ?? "";

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Code = code
            };

            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await GetUserByEmail(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    ModelState.Clear();
                    TempData["NotifySuccess"] = Resource.YourPasswordResetSuccessfully;
                    return RedirectToAction("login", "account");
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code ?? "", model.Password ?? "");
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    TempData["NotifySuccess"] = Resource.YourPasswordResetSuccessfully;
                    return RedirectToAction("login", "account");
                }
                TempData["NotifyFailed"] = Resource.FailedToResetPasswordPleaseTryAgainLater;
                AddErrors(result);
            }
            catch (Exception ex)
            {
                TempData["NotifyFailed"] = Resource.SomethingIsWrongPleaseTryAgainLater;
                LogCurrentControllerActionError(ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
            return RedirectToAction("index", "home");
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IdentityUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public string GetMyProfilePictureName(string userid)
        {
            string fileName = "";
            if (!string.IsNullOrEmpty(userid))
            {
                string profilePictureTypeId = _generalService.GetGlobalOptionSetId(ProjectEnum.UserAttachment.ProfilePicture.ToString(), "UserAttachment");
                fileName = db.UserAttachments.Where(a => a.CreatedBy == userid && a.AttachmentTypeId == profilePictureTypeId).OrderByDescending(o => o.CreatedOn).Select(a => a.UniqueFileName).FirstOrDefault() ?? "";
            }
            return fileName;
        }

        public string GetRoleIdByRoleName(string roleName)
        {
            string? roleId = db.Roles.Where(a => a.Name == roleName).Select(a => a.Id).FirstOrDefault();
            if (!string.IsNullOrEmpty(roleId))
            {
                return roleId;
            }
            return "";
        }

        public async Task<UserProfileViewModel> GetUsernameAndEmail(string userId)
        {
            UserProfileViewModel userProfile = new UserProfileViewModel();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                userProfile.Username = user.UserName ?? "";
                userProfile.EmailAddress = user.Email ?? "";
            }
            return userProfile;
        }

        public async Task<string> GetCurrentUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null && roles.Count > 0)
                {
                    return roles.FirstOrDefault() ?? "";
                }
            }

            return "";
        }

        public async Task<UserProfileViewModel> GetCurrentUserProfile(string upId)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            string profilePicTypeId = _generalService.GetGlobalOptionSetId(ProjectEnum.UserAttachment.ProfilePicture.ToString(), "UserAttachment");
            model = await (from t1 in db.UserProfiles
                           where t1.Id == upId
                           select new UserProfileViewModel
                           {
                               Id = t1.Id,
                               AspNetUserId = t1.AspNetUserId ?? "",
                               FullName = t1.FullName ?? "",
                               IDCardNumber = t1.IDCardNumber ?? "",
                               FirstName = t1.FirstName ?? "",
                               LastName = t1.LastName ?? "",
                               PhoneNumber = t1.PhoneNumber ?? "",
                               Address = t1.Address ?? "",
                               PostalCode = t1.PostalCode ?? "",
                               UserStatusId = t1.UserStatusId ?? "",
                               CountryName = t1.CountryName ?? "",
                               CreatedBy = t1.CreatedBy ?? "",
                               ModifiedBy = t1.ModifiedBy ?? "",
                               CreatedOn = t1.CreatedOn,
                               ModifiedOn = t1.ModifiedOn,
                               IsoUtcCreatedOn = t1.IsoUtcCreatedOn ?? "",
                               IsoUtcModifiedOn = t1.IsoUtcModifiedOn ?? ""
                           }).FirstOrDefaultAsync() ?? new UserProfileViewModel();
            UserProfileViewModel usernameEmail = await GetUsernameAndEmail(model.AspNetUserId ?? "");
            model.Username = usernameEmail.Username;
            model.EmailAddress = usernameEmail.EmailAddress;
            model.UserStatusName = db.GlobalOptionSets.Where(a => a.Id == model.UserStatusId).Select(a => a.DisplayName).FirstOrDefault() ?? "";
            model.UserRoleName = await GetCurrentUserRole(model.AspNetUserId ?? "");
            model.ProfilePictureFileName = db.UserAttachments.Where(a => a.CreatedBy == model.AspNetUserId && a.AttachmentTypeId == profilePicTypeId).OrderByDescending(o => o.CreatedOn).Select(a => a.UniqueFileName).FirstOrDefault() ?? "";
            return model;
        }

        public void SaveLoginHistory(string userId)
        {
            var loginHistory = new LoginHistory
            {
                AspNetUserId = userId,
                LoginDateTime = _generalService.GetSystemTimeZoneDateTimeNow(),
                IsoUtcLoginDateTime = _generalService.GetIsoUtcNow()
            };

            db.LoginHistories.Add(loginHistory);
            db.SaveChanges();
        }

        public async Task<bool> HaveAnyUserInSystem()
        {
            return await _userManager.Users.AnyAsync();
        }

        public void RegisterUserProfile(string userId, string? fullname, string? countryCode, string? regionCode, string? timezoneOffset)
        {
            try
            {
                UserProfile userProfile = new()
                {
                    AspNetUserId = userId,
                    FullName = fullname,
                    UserStatusId = _generalService.GetGlobalOptionSetId(ProjectEnum.UserStatus.Registered.ToString(), "UserStatus")
                };
                _generalService.SetCreatedInfo(userProfile, userId);
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogCurrentControllerActionError(ex);
            }
        }

        public async Task RegisterUser(RegisterViewModel model, bool confirmEmailToLogin, string role)
        {
            bool usernameExist = await _userManager.FindByNameAsync(model.UserName) != null;
            bool emailExist = await _userManager.FindByEmailAsync(model.Email) != null;
            if (usernameExist)
            {
                ModelState.AddModelError("UserName", Resource.UsernameHasBeenTaken);
            }
            if (emailExist)
            {
                ModelState.AddModelError("Email", Resource.EmailAddressHasBeenTaken);
            }
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = !confirmEmailToLogin
                };

                var result = await _userManager.CreateAsync(user, model.Password); //create user and save in db
                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByNameAsync(user.UserName);

                    if (createdUser != null)
                    {
                        //create user profile
                        RegisterUserProfile(createdUser.Id, model.FullName, model.CountryCode, model.RegionCode, model.TimezoneOffset);

                        await _userManager.AddToRoleAsync(createdUser, role);

                        // Send an email with this link
                        if (confirmEmailToLogin)
                        {
                            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            string callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme) ?? "";
                            string fullName = db.UserProfiles.Where(a => a.AspNetUserId == user.Id).Select(a => a.FullName).FirstOrDefault() ?? "";
                            EmailTemplate? emailTemplate = _emailService.EmailTemplateForConfirmEmail(fullName, callbackUrl ?? "");
                            await _emailService.SendEmailAsync(user.Email, emailTemplate?.Subject ?? "", emailTemplate?.Body ?? "", user.Id);
                        }
                    }
                }

                if (result.Errors != null)
                {
                    foreach (var message in result.Errors)
                    {
                        if (message.Description.Contains("Email"))
                        {
                            ModelState.AddModelError("Email", message.Description);
                        }
                        if (message.Description.Contains("UserName"))
                        {
                            ModelState.AddModelError("UserName", message.Description);
                        }
                        if (message.Description.Contains("Password"))
                        {
                            ModelState.AddModelError("Password", message.Description);
                        }
                        if (message.Description.Contains("ConfirmPassword"))
                        {
                            ModelState.AddModelError("ConfirmPassword", message.Description);
                        }
                    }
                }
            }
        }
    }
}
