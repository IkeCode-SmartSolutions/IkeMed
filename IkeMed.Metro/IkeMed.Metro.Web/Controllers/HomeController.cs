using IkeMed.Metro.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IkeMed.Metro.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            base.SetPageTitle("Inicio");
            base.SetPageSmallTitle("teste");

            return View();
        }
    }
}