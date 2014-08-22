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
        [Required]
        public DateTime AdmissionDate { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
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
