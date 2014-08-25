using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Doctor : BaseModel
    {
        [Required, Display(Name = "Data de Admissão"), DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; }

        [Required, Display(Name = "Data de Aniversário"), DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Imagem de Perfil")]
        public string ProfileImage { get; set; }

        public virtual Person Person { get; set; }

        public Doctor()
            : base()
        {
            this.AdmissionDate = (DateTime)SqlDateTime.MinValue;
            this.Birthdate = (DateTime)SqlDateTime.MinValue;
        }
    }
}
