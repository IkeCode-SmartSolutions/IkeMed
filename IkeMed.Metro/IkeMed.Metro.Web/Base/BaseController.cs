using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IkeMed.Metro.Web.Base
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.SetBackLink(null);
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