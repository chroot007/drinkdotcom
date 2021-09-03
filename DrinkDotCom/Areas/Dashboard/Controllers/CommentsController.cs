using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class CommentsController : Controller
    {

        private DrinkDotComUserManager _userManager;
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

        public async Task<ActionResult> Index(string userID, string searchTerm, int? pageNo, int entityID = (int)EntityEnums.Product, bool isPartial = false)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;
            pageNo = pageNo ?? 1;

            CommentsListingViewModel model = new CommentsListingViewModel();
            model.SearchTerm = searchTerm;

            if (!string.IsNullOrEmpty(userID))
            {
                model.User = await UserManager.FindByIdAsync(userID);
            }

            model.Comments = CommentsService.Instance.GetComments(userID, searchTerm, entityID, pageNo, pageSize);

            if (model.Comments != null && model.Comments.Count > 0)
            {
                var productIDs = model.Comments.Select(x => x.RecordID).ToList();

                model.CommentedProducts = ProductsService.Instance.GetProductsByIDs(productIDs);
            }

            var totalCount = CommentsService.Instance.GetCommentsTotalCount(userID, searchTerm, entityID);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            if (Request.IsAjaxRequest() || isPartial)
            {
                return PartialView("_UserComments", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult LeaveComment(CommentViewModel model)
        {
            JsonResult result = new JsonResult();

            try
            {
                var comment = new Comment();
                comment.Text = model.Text;
                comment.Rating = model.Rating;
                comment.EntityID = model.EntityID;
                comment.RecordID = model.RecordID;
                comment.UserID = User.Identity.GetUserId();
                comment.TimeStamp = DateTime.Now;

                var res = CommentsService.Instance.AddComment(comment);

                result.Data = new { Success = res };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            var comment = CommentsService.Instance.GetComment(ID);

            if (comment != null && User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || comment.UserID == User.Identity.GetUserId()))
            {
                result.Data = new { Success = CommentsService.Instance.DeleteComment(comment.ID), Message = "" };
            }
            else
            {
                result.Data = new { Success = false, Message = "You are Unauthorized to perform this action." };
            }

            return result;
        }
    }
}