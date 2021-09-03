using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.Services;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class UsersController : BaseController
    {
        private DrinkDotComUserManager _userManager;
        private DrinkDotComRoleManager _roleManager;
        private DrinkDotComSignInManager _signInManager;

        public UsersController()
        {
        }

        public UsersController(DrinkDotComUserManager userManager, DrinkDotComRoleManager roleManager, DrinkDotComSignInManager signInManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

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

        public ActionResult Index(string roleID, string searchTerm, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            UsersListingViewModel model = new UsersListingViewModel();
            model.PageTitle = "Users";
            model.PageDescription = "Users Listing Page";

            model.RoleID = roleID;
            model.SearchTerm = searchTerm;

            model.Roles = RoleManager.Roles.ToList();

            var users = UserManager.Users;

            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(x => x.Roles.FirstOrDefault(y => y.RoleId == roleID) != null);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()) || x.UserName.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            model.Users = users.OrderBy(x => x.Email).Skip(skipCount).Take(pageSize).ToList();

            model.Pager = new Pager(users.Count(), pageNo, pageSize);

            return View(model);
        }

        public ActionResult Create()
        {


            return View();
        }

        public async Task<ActionResult> UserDetails(string userID, bool isPartial = false)
        {
            UserDetailsViewModel model = new UserDetailsViewModel();

            var user = await UserManager.FindByIdAsync(userID);

            if (user != null)
            {
                model.User = user;
            }

            if (isPartial || Request.IsAjaxRequest())
            {
                return PartialView("_UserDetails", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Action(UserDetailsViewModel model)
        {
            JsonResult jResult = new JsonResult();

            if (model != null)
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.Address = model.Address;
                    user.ZipCode = model.ZipCode;

                    var result = await UserManager.UpdateAsync(user);

                    jResult.Data = new { Success = result.Succeeded };

                    return jResult;
                }
            }

            jResult.Data = new { Success = false };
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string userID)
        {
            JsonResult jResult = new JsonResult();

            if (!string.IsNullOrEmpty(userID))
            {
                var user = await UserManager.FindByIdAsync(userID);

                if (user != null)
                {
                    var result = await UserManager.DeleteAsync(user);

                    jResult.Data = new { Success = result.Succeeded };

                    return jResult;
                }
            }

            jResult.Data = new { Success = false };
            return jResult;
        }
    }
}