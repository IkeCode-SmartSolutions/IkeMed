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

        private RegisterPersonViewModel()
            : base()
        {
            this.Person = new Person();
        }

        public RegisterPersonViewModel(PersonTypeEnum personType)
            : this()
        {
            this.PersonType = personType;
            this.Person = new Person();
        }

        public void SetPerson(Person person)
        {
            this.Person = person;
        }
    }
}