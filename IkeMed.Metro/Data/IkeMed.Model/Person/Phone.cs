using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Phone : BaseModel
    {
        [Required]
        [MaxLength(30)]
        [Index("IX_PHONE_NUMBER", IsUnique = true)]
        public string Number { get; set; }
        [Required]
        public int PhoneType { get; set; }

        public virtual Person Person { get; set; }

        public Phone()
            : base()
        {
        }
    }
}
