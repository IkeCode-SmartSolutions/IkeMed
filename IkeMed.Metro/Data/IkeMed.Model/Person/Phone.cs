using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Phone : BaseModel
    {
        [Required]
        [Display(Name = "Número"), MaxLength(30), DataType(DataType.PhoneNumber)]
        [Index("IX_PHONE_NUMBER", IsUnique = true)]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Tipo de Telefone")]
        public PhoneTypeEnum PhoneType { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public Phone()
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
