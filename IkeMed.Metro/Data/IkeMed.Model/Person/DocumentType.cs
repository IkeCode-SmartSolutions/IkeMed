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
    public class DocumentType : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public DocumentType()
            : base()
        {
        }

        protected override void SetEntitiesState(IkeMedContext context)
        {
            if (this != null)
            {
                context.Entry(this).State = this.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (this.Documents != null)
                    context.Entry(this.Documents).State = this.Documents != null && this.Documents.Count > 0
                                                            ? EntityState.Modified : EntityState.Added;
            }
        }
    }
}
