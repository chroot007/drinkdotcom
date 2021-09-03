using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Services;
using DrinkDotCom.Entities;

namespace DrinkDotCom.Controllers
{
    [Authorize]
    public class SharedController : Controller
    {
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/Content/images/") + fileName;

                picture.SaveAs(path);

                var dbPicture = new Picture();
                dbPicture.URL = fileName;

                int pictureID = SharedService.Instance.SavePicture(dbPicture);

                picturesJSON.Add(new { ID = pictureID, pictureURL = fileName });
            }

            result.Data = picturesJSON;

            return result;
        }

        [HttpPost]
        public JsonResult UploadPicturesWithoutDatabase(string subFolder, bool isSiteFolder = false)
        {
            JsonResult result = new JsonResult();

            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var folderPath = string.Format("~/Content/images/{0}{1}", isSiteFolder ? "site/" : string.Empty, !string.IsNullOrEmpty(subFolder) ? subFolder + "/" : string.Empty);

                var path = Server.MapPath(folderPath) + fileName;

                picture.SaveAs(path);

                picturesJSON.Add(new { pictureURL = string.Format("{0}{1}", folderPath.Replace("~", ""), fileName), pictureValue = string.Format("{0}{1}", folderPath.Replace("~/Content/images/", ""), fileName) });
            }

            result.Data = picturesJSON;

            return result;
        }
    }
}