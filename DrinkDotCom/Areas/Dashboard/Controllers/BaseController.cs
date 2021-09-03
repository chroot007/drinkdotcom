using DrinkDotCom.Utilities.Code.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{

    [CustomAuthorize(Roles = "Administrator")]
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;

#if DEBUG
            throw ex;
#endif

            ////Log Exception e
            //filterContext.ExceptionHandled = true;
            //filterContext.Result = new ViewResult()
            //{
            //    ViewName = "Error"
            //};
        }

        [AllowAnonymous]
        public ActionResult UnAuthorized()
        {
            Response.StatusCode = 403;

            return PartialView("UnAuthorized");
        }
    }
}