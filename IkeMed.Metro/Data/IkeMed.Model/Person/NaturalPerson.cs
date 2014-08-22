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
    public class NaturalPerson : BaseModel
    {
        [Required]
        public int Gender { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public string ProfileImage { get; set; }

        public virtual Person Person { get; set; }

        public NaturalPerson()
            : base()
        {
            this.Birthdate = (DateTime)SqlDateTime.MinValue;
        }
    }
}
