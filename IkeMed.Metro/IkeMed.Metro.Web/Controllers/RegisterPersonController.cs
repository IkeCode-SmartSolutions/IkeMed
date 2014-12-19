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
using System.Data.Entity.Migrations;
using System.Data.Entity;
using IkeCode;
using Newtonsoft.Json;

namespace IkeMed.Metro.Web.Controllers
{
    public class RegisterPersonController : BaseController
    {
        public RegisterPersonController()
            : base()
        {
        }

        public ActionResult Index(int id = 0)
        {
            var vm = new RegisterPersonViewModel();

            base.SetPageTitle("Cadastro");
            base.SetPageSmallTitle("Pessoa");

            if (id > 0)
            {

                this.Run<RegisterPersonViewModel>(string.Format("RegisterPersonController.Index(id={0})", id), () =>
                {
                    using (var context = new IkeMedContext())
                    {
                        var person = context.People
                                        .Where(i => i.ID == id)
                                        .Include(i => i.NaturalPerson)
                                        .Include(i => i.LegalPerson)
                                        .Include(i => i.Doctor)
                                        .Include(i => i.Addresses)
                                        .Include(i => i.Documents.Select(s => s.DocumentType))
                                        .Include(i => i.Phones)
                                        .FirstOrDefault();

                        if (person != null)
                        {
                            base.SetPageTitle("Editar");
                        }
                        vm.Person = person;
                    }
                    return vm;
                });
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Post(RegisterPersonViewModel person)
        {
            base.SetPageTitle("Editar");
            base.SetPageSmallTitle("Pessoa");

            var vm = new RegisterPersonViewModel();
            this.Run<RegisterPersonViewModel>(string.Format("RegisterPersonController.Post(id={0})", person.Person.ID), () =>
            {
                //using (var context = new IkeMedContext())
                //{
                //    person.SaveChanges(context);
                //}

                //vm.Person = person;

                return vm;
            });

            vm.Notify = new NotifyViewModel("Cadastro salvo com sucesso!");

            return View("Index", vm);
        }
    }
}