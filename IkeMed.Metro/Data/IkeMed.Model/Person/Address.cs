using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Address : BaseModel
    {
        [Required]
        [MaxLength(250)]
        public string Street { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Number { get; set; }
        
        [MaxLength(50)]
        public string Complement { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Neighborhood { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(3)]
        public string State { get; set; }
        
        [Required]
        public AddressTypeEnum AddressType { get; set; }
        
        public virtual Person Person { get; set; }

        public Address()
            : base()
        {
        }

        protected override void SetEntitiesState(IkeMedContext context)
        {
            if (this != null)
            {
                context.Entry(this).State = this.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (this.Person != null)
                    context.Entry(this.Person).State = this.Person != null && this.Person.ID > 0
                                                            ? EntityState.Modified : EntityState.Added;
            }
        }
    }
}
