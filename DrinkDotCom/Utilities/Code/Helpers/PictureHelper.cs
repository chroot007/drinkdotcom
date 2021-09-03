using DrinkDotCom.Entities;
using ClassLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Utilities.Code.Helpers
{
    public static class PictureHelper
    {
        public static MvcHtmlString Picture(this HtmlHelper htmlHelper, Picture picture, string classes = "", string style = "", string alt = "")
        {
            var picURL = picture != null ? picture.URL : "";

            return Picture(htmlHelper, picURL, classes, style, alt);
        }
        public static MvcHtmlString PictureWithZoomAttribute(this HtmlHelper htmlHelper, Picture picture, string classes = "", string style = "", string alt = "", string attributeName = "data-zoom", string id = "")
        {
            var picURL = picture != null ? picture.URL : "";

            return PictureWithZoomAttribute(htmlHelper, picURL, classes, style, alt, attributeName: attributeName, id: id);
        }

        public static MvcHtmlString Picture(this HtmlHelper htmlHelper, string pictureURL, string classes = "", string style = "", string alt = "", string defaultPic = "site/default-picture.png")
        {
            pictureURL = string.IsNullOrEmpty(pictureURL) ? defaultPic : pictureURL;

            var image = new TagBuilder("img");
            image.AddCssClass(classes);
            image.MergeAttribute("style", style);
            image.MergeAttribute("src", string.Format("/content/images/{0}", pictureURL));
            image.MergeAttribute("alt", alt);

            return MvcHtmlString.Create(image.ToString());
        }
        public static MvcHtmlString PictureWithZoomAttribute(this HtmlHelper htmlHelper, string pictureURL, string classes = "", string style = "", string alt = "", string defaultPic = "site/default-picture.png", string attributeName = "data-zoom", string id = "")
        {
            pictureURL = string.IsNullOrEmpty(pictureURL) ? defaultPic : pictureURL;

            var image = new TagBuilder("img");
            image.AddCssClass(classes);
            image.MergeAttribute("style", style);
            image.MergeAttribute("src", string.Format("/content/images/{0}", pictureURL));
            image.MergeAttribute(attributeName, string.Format("/content/images/{0}", pictureURL));
            image.MergeAttribute("id", id);
            image.MergeAttribute("alt", alt);

            return MvcHtmlString.Create(image.ToString());
        }

        public static MvcHtmlString UserAvatar(this HtmlHelper htmlHelper, Picture picture, string classes = "", string style = "", string alt = "")
        {
            var picURL = picture != null ? picture.URL : "";

            return Picture(htmlHelper, picURL, classes, style, alt, "site/user-default-avatar.png");
        }
        
        public static MvcHtmlString UserAvatar(this HtmlHelper htmlHelper, string pictureURL, string classes = "", string style = "", string alt = "")
        {
            return Picture(htmlHelper, pictureURL, classes, style, alt, "site/user-default-avatar.png");
        }

        public static string PictureSource(this HtmlHelper htmlHelper, string pictureURL)
        {
            return string.Format("/content/images/{0}", pictureURL);
        }

        public static string PageImageURL(string imagePath, bool isCompletePath = false)
        {
            return string.Format("/content/images/{0}{1}", isCompletePath ? string.Empty : "site/pages/", imagePath).ToSiteURL();
        }
        public static string UserAvatarSource(Picture picture)
        {
            var picURL = picture != null ? picture.URL : "";

            return string.Format("/content/images/{0}", !string.IsNullOrEmpty(picURL) ? picURL : "site/user-default-avatar.png").ToSiteURL();
        }
    }
}