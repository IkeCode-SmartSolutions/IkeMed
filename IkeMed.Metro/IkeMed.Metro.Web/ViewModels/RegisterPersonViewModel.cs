using IkeMed.Metro.Web.Base;
using IkeMed.Model;
using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IkeMed.Metro.Web.ViewModels
{
    public class RegisterPersonViewModel : BaseViewModel
    {
        public Person Person { get; set; }

        public RegisterPersonViewModel()
            : base()
        {
            this.Person = new Person();
            this.Person.Doctor = this.Person.Doctor ?? new Doctor();
            this.Person.NaturalPerson = this.Person.NaturalPerson ?? new NaturalPerson();
            this.Person.LegalPerson = this.Person.LegalPerson ?? new LegalPerson();

            this.Person.Phones = this.Person.Phones ?? new List<Phone>();
            this.Person.Documents = this.Person.Documents ?? new List<Document>();
            this.Person.Addresses = this.Person.Addresses ?? new List<Address>();

        }

        public RegisterPersonViewModel(Person person)
            : this()
        {
            this.Person = person;
        }
    }
}