using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class Document : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Value { get; set; }
        
        public DocumentType DocumentType { get; set; }
        
        public virtual Person Person { get; set; }

        public Document()
            : base()
        {
        }
    }
}
