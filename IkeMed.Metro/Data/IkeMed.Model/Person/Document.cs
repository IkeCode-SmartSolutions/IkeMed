﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
