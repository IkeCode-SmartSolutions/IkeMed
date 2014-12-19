using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IkeMed.Model
{
    public class NaturalPerson : BaseModel
    {
        [Display(Name = "Sexo")]
        [Required]
        public GenderEnum Gender { get; set; }

        [Display(Name = "Data de Aniversário"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required, DataType(DataType.Date)]
        [MinDate("01/01/1930", true)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Imagem de Perfil")]
        [NotMapped]
        public HttpPostedFileWrapper ProfileImage { get; set; }

        [Display(Name = "Url da Imagem de Perfil")]
        public string ProfileImageUrl { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public NaturalPerson()
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
