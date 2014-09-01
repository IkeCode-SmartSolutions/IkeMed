using IkeMed.Model.Enums;
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
        [Display(Name = "Tipo de Pessoa")]
        public PersonTypeEnum PersonType { get; private set; }

        [Display(Name = "Data de Admissão"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required, DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Imagem de Perfil")]
        public string ProfileImage { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public Doctor()
            : base()
        {
            this.PersonType = PersonTypeEnum.Doctor;
            this.AdmissionDate = (DateTime)SqlDateTime.MinValue;
        }
    }
}
