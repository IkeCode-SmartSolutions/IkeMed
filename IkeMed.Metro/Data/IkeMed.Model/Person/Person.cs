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
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        [Index("IX_PEOPLE_EMAIL", IsUnique = true)]
        public string Email { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual LegalPerson LegalPerson { get; set; }
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
