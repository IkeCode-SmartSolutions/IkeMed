using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IkeMed.Model
{
    public class LegalPerson : BaseModel
    {
        [Display(Name = "Tipo de Pessoa")]
        public PersonTypeEnum PersonType { get; private set; }

        [Required]
        [Display(Name = "Nome Fantasia"), MaxLength(250)]
        public string SocialName { get; set; }
        
        [Required]
        [Display(Name = "Razão Social"), MaxLength(250)]
        public string CompanyName { get; set; }

        [Display(Name = "Imagem de Perfil")]
        [NotMapped]
        public HttpPostedFileWrapper ProfileImage { get; set; }

        [Display(Name = "Url da Imagem de Perfil")]
        public string ProfileImageUrl { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public LegalPerson()
            : base()
        {
            this.PersonType = PersonTypeEnum.Legal;
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
