using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class LegalPerson : BaseModel
    {
        [Required]
        [MaxLength(250)]
        public string SocialName { get; set; }
        [Required]
        [MaxLength(250)]
        public string CompanyName { get; set; }
        public string ProfileImage { get; set; }

        public virtual Person Person { get; set; }

        public LegalPerson()
            : base()
        {
        }
    }
}
