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
        public PersonTypeEnum PersonType { get; private set; }
        public Person Person { get; private set; }

        public RegisterPersonViewModel()
            : base()
        {
            this.Person = new Person();
            this.Person.Doctor = this.Person.Doctor ?? new Doctor();
            this.Person.NaturalPerson = this.Person.NaturalPerson ?? new NaturalPerson();
            this.Person.LegalPerson = this.Person.LegalPerson ?? new LegalPerson();
            this.PersonType = PersonTypeEnum.None;
        }

        public RegisterPersonViewModel(PersonTypeEnum personType)
            : this()
        {
            this.PersonType = personType;
        }

        public void SetPerson(Person person)
        {
            person.Doctor = person.Doctor ?? new Doctor();
            person.NaturalPerson = person.NaturalPerson ?? new NaturalPerson();
            person.LegalPerson = person.LegalPerson ?? new LegalPerson();

            this.Person = person;
        }
    }
}