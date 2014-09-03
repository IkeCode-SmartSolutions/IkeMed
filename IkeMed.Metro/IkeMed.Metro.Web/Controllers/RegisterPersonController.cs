using IkeCode.Core.CustomAttributes;
using IkeCode.Core.CustomAttributes.Helpers;
using IkeMed.Metro.Web.Base;
using IkeMed.Metro.Web.ViewModels;
using IkeMed.Model;
using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Index(string personType, int id = 0, bool saved = false)
        {
            PersonTypeEnum type = personType.GetEnumRouteNameAttribute<PersonTypeEnum>();
            var vm = new RegisterPersonViewModel(type);

            base.SetPageTitle("Cadastro");
            base.SetPageSmallTitle("Pessoa");

            if (id > 0)
            {
                var person = (from p in base.IkeMedContext.People
                              where p.ID == id
                              select p).FirstOrDefault();

                if (person != null)
                {
                    base.SetPageTitle("Editar");
                    vm.SetPerson(person);
                }
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            if (person.NaturalPerson.ProfileImage != null && person.NaturalPerson.ProfileImage.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Uploads/ProfileImages"), Path.GetFileName(person.NaturalPerson.ProfileImage.FileName));
                //string url = Url.Content("~/Uploads/ProfileImages", Path.GetFileName(person.NaturalPerson.ProfileImage.FileName));
                //person.NaturalPerson.ProfileImageUrl = url;
                person.NaturalPerson.ProfileImage.SaveAs(filePath);
            }

            var vm = new RegisterPersonViewModel(PersonTypeEnum.Doctor);
            vm.SetPerson(person);
            return View("Index", vm);

            //return RedirectToRoute("RegisterPerson",
            //    new
            //    {
            //        personType = PersonTypeEnum.Doctor.GetEnumRouteNameAttributeValue(),
            //        id = person.ID
            //    });
            //return Json(new { success = success, person = person }, JsonRequestBehavior.AllowGet);
        }
    }
}