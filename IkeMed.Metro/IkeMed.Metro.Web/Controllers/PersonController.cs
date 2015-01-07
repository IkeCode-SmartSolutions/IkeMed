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
using IkeMed.Metro.Web.Models;

namespace IkeMed.Metro.Web.Controllers
{
    public class PersonController : BaseController
    {
        public PersonController()
            : base()
        {
        }

        public ActionResult Index(int id = 0)
        {
            var vm = new PersonViewModel();

            base.SetPageTitle("Cadastro");
            base.SetPageSmallTitle("Pessoa");

            if (id > 0)
            {
                this.Run<PersonViewModel>(string.Format("PersonController.Index(id={0})", id),
                    () =>
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

                            if (person == null)
                                return null;

                            base.SetPageTitle("Editar");
                            vm.Person = person;
                        }
                        return vm;
                    });
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            base.SetPageTitle("Editar");
            base.SetPageSmallTitle("Pessoa");

            var vm = new PersonViewModel();
            if (person != null)
            {
                this.Run(string.Format("PersonController.Post(id={0})", person.ID),
                    () =>
                    {
                        using (var context = new IkeMedContext())
                        {
                            person.SaveChanges(context);
                            vm.Person = context.People
                                            .Where(i => i.ID == person.ID)
                                            .Include(i => i.NaturalPerson)
                                            .Include(i => i.LegalPerson)
                                            .Include(i => i.Doctor)
                                            .Include(i => i.Addresses)
                                            .Include(i => i.Documents.Select(s => s.DocumentType))
                                            .Include(i => i.Phones)
                                            .FirstOrDefault();
                        }
                    });
                vm.Notify = new NotifyViewModel("Cadastro salvo com sucesso!");
            }
            else
            {
                vm.Notify = new NotifyViewModel("Ocorreu um problema ao salvar o registro!", theme: "red");
            }
            return View("Index", vm);
        }

        [HttpPost]
        public JsonResult PostAddress(Address address)
        {
            var count = 0;
            using (var context = this.Context)
            {
                if (address.ID == 0)
                {
                }
                context.Addresses.AddOrUpdate(addr => addr.ID, address);
                count = context.SaveChanges();
            }
            if (count > 0)
                return Json(new { Result = "OK", Record = address }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Result = "ERROR", Message = "Não foi possivel atualizar o registro, por favor tente novamente." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateAddress(Address address)
        {
            return Json(new { Result = "OK", Record = address }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAddress(int id)
        {
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePhone(Phone phone)
        {
            return Json(new { Result = "OK", Record = phone }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreatePhone(Phone phone)
        {
            return Json(new { Result = "OK", Record = phone }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePhone(int id)
        {
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDocument(Document document)
        {
            return Json(new { Result = "OK", Record = document }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateDocument(Document document)
        {
            return Json(new { Result = "OK", Record = document }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDocument(int id)
        {
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentTypes()
        {
            using (var context = new IkeMedContext())
            {
                var docTypes = context.DocumentTypes.ToList();
                var result = new Dictionary<string, object>(docTypes.Count);
                foreach (var doc in docTypes)
                {
                    result.Add(doc.Name, doc.ID);
                }
                
                return Json(new { Result = "OK", Options = JTableOptionModel.GetModelList(result) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}