using IkeCode.Core.CustomAttributes;
using IkeCode.Core.CustomAttributes.Helpers;
using IkeMed.Metro.Web.Base;
using IkeMed.Metro.Web.ViewModels;
using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace IkeMed.Metro.Web.Controllers
{
    public class RegisterPersonController : BaseController
    {
        public RegisterPersonController()
            : base()
        {

        }

        public ActionResult Index(string personType, int id = 0)
        {
            PersonTypeEnum type = personType.GetEnumRouteNameAttribute<PersonTypeEnum>();

            base.SetPageTitle("Cadastro");
            base.SetPageSmallTitle(type.GetDisplayName<PersonTypeEnum>());

            var vm = new RegisterPersonViewModel(type);

            if (id > 0)
            {
                var person = (from p in base.IkeMedContext.People
                                 where p.ID == id
                                 select p).SingleOrDefault();
                vm.SetPerson(person);
            }

            return View(vm);
        }
    }
}