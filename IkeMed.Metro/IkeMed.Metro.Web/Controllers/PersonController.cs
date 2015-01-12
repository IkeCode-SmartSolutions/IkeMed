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
                        using (var context = this.Context)
                        {
                            if (person.ID > 0)
                            {
                                context.People.AddOrUpdate(i => i.ID, person);
                                context.SaveChanges();
                            }
                            else
                            {
                                context.People.Add(person);
                                person.SaveChanges(context, person);
                            }
                            //vm.Person = person;
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
        public JsonResult PostAddress(Address address, int personId)
        {
            if (address.ID <= 0 && personId <= 0)
            {
                throw new ArgumentException("Parameter -> personId is required");
            }

            return Json(
                base.RunJTableResult<Address>(() =>
            {
                using (var context = this.Context)
                {
                    if (address.ID > 0)
                    {
                        context.Addresses.AddOrUpdate(i => i.ID, address);
                        context.SaveChanges();
                    }
                    else
                    {
                        var person = context.People.Where(p => p.ID == personId).FirstOrDefault();
                        if (person != null)
                        {
                            person.Addresses.Add(address);
                        }

                        context.SaveChanges();
                        //Remove circular reference
                        address.Person = null;
                    }
                }
            }, address)
            , JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAddress(int id)
        {
            return Json(
                base.RunJTableResult<Address>(() =>
                {
                    if (id <= 0)
                        throw new ArgumentException("Parameter -> id[Address.ID] is required");

                    using (var context = this.Context)
                    {
                        var addr = context.Addresses.FirstOrDefault(i => i.ID == id);
                        context.Entry(addr).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }, null)
            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostPhone(Phone phone, int personId)
        {
            return Json(
                base.RunJTableResult<Phone>(() =>
                {
                    if (phone.ID <= 0 && personId <= 0)
                    {
                        throw new ArgumentException("Parameter -> personId is required");
                    }

                    using (var context = this.Context)
                    {
                        if (phone.ID > 0)
                        {
                            context.Phones.AddOrUpdate(i => i.ID, phone);
                            context.SaveChanges();
                        }
                        else
                        {
                            var person = context.People.Where(p => p.ID == personId).FirstOrDefault();
                            if (person != null)
                            {
                                person.Phones.Add(phone);
                            }

                            context.SaveChanges();
                            //Remove circular reference
                            phone.Person = null;
                        }
                    }
                }, phone),
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePhone(int id)
        {
            return Json(
                base.RunJTableResult<Phone>(() =>
                {
                    if (id <= 0)
                        throw new ArgumentException("Parameter -> id[Phone.ID] is required");

                    using (var context = this.Context)
                    {
                        var phone = context.Phones.FirstOrDefault(i => i.ID == id);
                        context.Entry(phone).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }, null)
        , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostDocument(Document document, int personId)
        {
            return Json(
                base.RunJTableResult<Document>(() =>
                {
                    if (document.ID <= 0 && personId <= 0)
                    {
                        throw new ArgumentException("Parameter -> personId is required");
                    }

                    using (var context = this.Context)
                    {
                        if (document.ID > 0)
                        {
                            context.Documents.AddOrUpdate(i => i.ID, document);
                            context.SaveChanges();
                        }
                        else
                        {
                            var person = context.People.Where(p => p.ID == personId).FirstOrDefault();
                            if (person != null)
                            {
                                person.Documents.Add(document);
                            }

                            context.SaveChanges();
                            //Remove circular reference
                            document.Person = null;
                        }
                    }
                }, document)
            , JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDocument(int id)
        {
            return Json(
                base.RunJTableResult<Document>(() =>
                {
                    if (id <= 0)
                        throw new ArgumentException("Parameter -> id[Document.ID] is required");

                    using (var context = this.Context)
                    {
                        var addr = context.Addresses.FirstOrDefault(i => i.ID == id);
                        context.Entry(addr).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }, null)
            , JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentTypes()
        {
            using (var context = base.Context)
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