using IkeCode.Core.Log;
using IkeMed.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IkeMed.Metro.Web.Base
{
    public class BaseController : Controller
    {
        protected static string BaseUrl { get; private set; }

        IkeMedContext _context;
        protected IkeMedContext Context
        {
            get
            {
                if (_context == null) 
                    _context = new IkeMedContext();
                return _context;
            }
        }

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

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            IkeCodeLog.Default.Exception(filterContext.Exception);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (_context != null)
                _context.Dispose();
            base.OnResultExecuted(filterContext);
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

        protected T Run<T>(string methodName, Func<T> runner)
        {
            var timeElapsed = new Stopwatch();

            try
            {
                timeElapsed.Start();

                var result = runner();
                timeElapsed.Stop();

                return result;
            }
            catch (Exception ex)
            {
                IkeCodeLog.Default.Exception(methodName, ex);
                throw;
            }
            finally
            {
                IkeCodeLog.Default.Metric(methodName, timeElapsed);
            }
        }
    }
}