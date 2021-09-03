using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Extensions;
using DrinkDotCom.Entities;
using DrinkDotCom.Models;
using DrinkDotCom.Services;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using reCAPTCHA.MVC;

namespace DrinkDotCom.Controllers
{


    [Authorize]
    [AllowSameSite]
    public class UsersController : Controller
    {
        #region Constructor and Managers
        private DrinkDotComSignInManager _signInManager;
        private DrinkDotComUserManager _userManager;
        private DrinkDotComRoleManager _roleManager;

        public DrinkDotComSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<DrinkDotComSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public DrinkDotComUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<DrinkDotComUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public DrinkDotComRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<DrinkDotComRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public UsersController()
        {
        }

        public UsersController(DrinkDotComUserManager userManager, DrinkDotComSignInManager signInManager, DrinkDotComRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        #endregion

        [AllowAnonymous]
        public ActionResult WebLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Home");
            }

            PageViewModel model = new PageViewModel();

            model.PageTitle = "Login or Register";
            model.PageDescription = string.Format("Login to your existing Account or Register new Account on {0}.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("WebLogin").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("account.jpg");

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 0)]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            JsonResult jsonResult = new JsonResult();

            var user = new DrinkDotComUser { FullName = model.FullName, UserName = model.Username, Email = model.Email, Dateofbirth = model.Dateofbirth , Country =model.Country, City = model.City, Address = model.Address, PhoneNumber = model.PhoneNumber};
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (await RoleManager.RoleExistsAsync("User"))
                {
                    //assign User role to newly registered user
                    await UserManager.AddToRoleAsync(user.Id, "User");
                }

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");                

                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Messages = string.Join("<br />", result.Errors) };
            }

            return jsonResult;
        }


        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 0)]
        public ActionResult Login()
        {
            return View();
        }
      

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            JsonResult jsonResult = new JsonResult();

            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    jsonResult.Data = new { Success = true, RequiresVerification = false };
                    break;
                case SignInStatus.RequiresVerification:
                    jsonResult.Data = new { Success = true, RequiresVerification = true };
                    break;
                case SignInStatus.LockedOut:
                    jsonResult.Data = new { Success = false, Messages = string.Format("You are locked out.") };
                    break;
                case SignInStatus.Failure:
                default:
                    jsonResult.Data = new { Success = false, Messages = string.Format("Invalid login attempt.") };
                    break;
            }

            return jsonResult;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SocialLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.SocialLoginCallback());
        }

        [AllowAnonymous]
        public async Task<ActionResult> SocialLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an existing account, then create an account
                    var user = new DrinkDotComUser { UserName = loginInfo.DefaultUserName, Email = loginInfo.Email };
                    var createUserResult = await UserManager.CreateAsync(user);
                    if (createUserResult.Succeeded)
                    {
                        createUserResult = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                        if (createUserResult.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToAction("Index", "Home");
                        }
                        else return View("Error");
                    }
                    else return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = !string.IsNullOrEmpty(model.Username) ? await UserManager.FindByNameAsync(model.Username) : null;

            if (user != null)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var callbackUrl = Url.Action("ResetPassword", "Users", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }

            // Don't reveal that the user does not exist or is not confirmed for security measures.
            // Just give a success message in every case.

            JsonResult jResult = new JsonResult();
            jResult.Data = new { Success = true };
            return jResult;
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string userId)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Code = code;
            model.UserId = userId;

            return string.IsNullOrEmpty(code) || string.IsNullOrEmpty(userId) ? View("Error") : View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            JsonResult jResult = new JsonResult();

            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

                if (!result.Succeeded)
                {
                    jResult.Data = new { Success = result.Succeeded, Messages = string.Join("\n", result.Errors) };
                }
                else
                {
                    jResult.Data = new { Success = result.Succeeded, Messages = "Your password has been reset. Please login with your updated credentials now." };
                }
            }
            else
            {
                jResult.Data = new { Success = false, Messages = "Unable to reset password." };
            }

            return jResult;
        }


        public ActionResult UserProfile()
        {
            ProfileDetailsViewModel model = new ProfileDetailsViewModel();

            model.User = UserManager.FindById(User.Identity.GetUserId());

            if (model.User == null) return HttpNotFound();

            model.PageTitle = "Profile";
            model.PageDescription = "Review your profile";
            model.PageURL = Url.UserProfile().ToSiteURL();
            model.PageImageURL = PictureHelper.UserAvatarSource(model.User.Picture);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserProfile", model);
            }
            else
            {
                return View(model);
            }
        }
        

        [HttpPost]
        public async Task<JsonResult> UpdateProfile(UpdateProfileDetailsViewModel model)
        {
            JsonResult jResult = new JsonResult();

            if (model != null && User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.UserName = model.Username;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.Address = model.Address;
                    user.ZipCode = model.ZipCode;

                    var result = await UserManager.UpdateAsync(user);

                    jResult.Data = new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) };

                    return jResult;
                }
            }
            else
            {
                jResult.Data = new { Success = false, Message = "Invalid User" };
            }

            return jResult;
        }

        public async Task<ActionResult> ChangePassword()
        {
            ProfileDetailsViewModel model = new ProfileDetailsViewModel();

            model.User = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return PartialView("_ChangePassword", model);
        }

        public async Task<JsonResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            JsonResult jResult = new JsonResult();

            if (model != null && User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                    jResult.Data = new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) };

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
            }
            else
            {
                jResult.Data = new { Success = false, Message = "Invalid User" };
            }

            return jResult;
        }

        public async Task<ActionResult> ChangeAvatar()
        {
            ProfileDetailsViewModel model = new ProfileDetailsViewModel();

            model.User = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return PartialView("_ChangeAvatar", model);
        }

        public async Task<JsonResult> UpdateAvatar(int pictureID)
        {
            JsonResult jResult = new JsonResult();

            if (pictureID > 0 && User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    user.PictureID = pictureID;

                    var result = await UserManager.UpdateAsync(user);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    jResult.Data = new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) };
                }
            }
            else
            {
                jResult.Data = new { Success = false, Message = "Invalid User" };
            }

            return jResult;
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

    }
}