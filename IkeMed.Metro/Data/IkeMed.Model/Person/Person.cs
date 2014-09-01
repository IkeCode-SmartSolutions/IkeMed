using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Person : BaseModel
    {
        [Required, MaxLength(250), Display(Name="Nome")]
        public string Name { get; set; }

        [Index("IX_PEOPLE_EMAIL", IsUnique = true)]
        [Required, MaxLength(250), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Médico")]
        public virtual Doctor Doctor { get; set; }

        [Display(Name = "Pessoa Jurídica")]
        public virtual LegalPerson LegalPerson { get; set; }

        [Display(Name = "Pessoa Física")]
        public virtual NaturalPerson NaturalPerson { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

        public Person()
            : base()
        {

        }
    }
}
