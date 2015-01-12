using IkeCode.Core.Log;
using IkeMed.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        protected void Run(string methodName, Action runner)
        {
            var timeElapsed = new Stopwatch();

            try
            {
                timeElapsed.Start();

                runner();

                timeElapsed.Stop();
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

        public object RunJTableResult<T>(Action action, T obj)
        {
            var errorMessage = new StringBuilder();
            try
            {
                action();

                return new { Result = "OK", Record = obj };
            }
            catch (DbEntityValidationException e)
            {
                var index = 0;
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var pattern = "{0}";
                        if (index > 0)
                            pattern = "<br/> {0}";

                        errorMessage.AppendLine(string.Format(pattern, ve.ErrorMessage));

                        index++;
                    }
                }

                return new { Result = "ERROR", Message = errorMessage.ToString() };
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
                {
                    return new
                    {
                        Result = "ERROR",
                        Message = @"Já existe um registro com os dados expecificados."
                    };
                }
                else
                {
                    return new
                    {
                        Result = "ERROR",
                        Message = sqlException.Message
                    };
                }
            }
            catch (UpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
                {
                    return new
                    {
                        Result = "ERROR",
                        Message = @"Já existe um registro com os dados expecificados."
                    };
                }
                else
                {
                    // it's something else...
                    throw;
                }
            }
            catch (ArgumentException)
            {
                return new { Result = "ERROR", Message = @"Ocorreu um erro ao tentar executar a operação.<br/><br/>
                                                                Se o problema persistir contate o suporte." };
            }
            catch (Exception e)
            {
                var message = e.InnerException == null || string.IsNullOrWhiteSpace(e.InnerException.Message) ? e.Message : e.InnerException.Message;
                return new { Result = "ERROR", Message = e.Message };
            }
        }
    }
}