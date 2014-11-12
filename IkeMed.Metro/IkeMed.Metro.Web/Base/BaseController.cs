using IkeMed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IkeMed.Metro.Web.Base
{
    public class BaseController : Controller
    {
        protected static string BaseUrl { get; private set; }

        public BaseController()
        {
            this.SetBackLink(null);
            this.SetPageTitle("");
            this.SetPageSmallTitle("");            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            BaseUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
        }

        #region Header Title

        protected void SetBackLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                ViewBag.BackLink = "/";
            else
                ViewBag.BackLink = link;
        }

        protected void SetPageTitle(string title)
        {
            ViewBag.PageTitle = title;
        }

        protected void SetPageSmallTitle(string title)
        {
            ViewBag.PageSmallTitle = title;
        }

        #endregion Header Title
    }
}