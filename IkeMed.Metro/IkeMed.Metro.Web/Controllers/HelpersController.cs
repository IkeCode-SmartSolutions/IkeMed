using IkeCode;
using IkeCode.Core.CustomAttributes;
using IkeMed.Metro.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace IkeMed.Metro.Web.Controllers
{
    public class HelpersController : Controller
    {
        public JsonResult GetJsonFromEnum(string enumName, string enumNamespace = "", string assemblyName = "")
        {
            var dic = Helpers.EnumToDictionary(enumName, enumNamespace, assemblyName);
            var options = JTableOptionModel.GetModelList(dic);

            return Json(new { Result = "OK", Options = options }, JsonRequestBehavior.AllowGet);
        }
    }
}