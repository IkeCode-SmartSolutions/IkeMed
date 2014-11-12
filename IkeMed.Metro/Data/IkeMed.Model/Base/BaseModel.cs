using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateIns { get; set; }
        public DateTime LastUpdate { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }

        public BaseModel()
        {
            this.IsActive = true;
        }

        #region Common Methods

        protected virtual void SetEntitiesState(IkeMedContext context)
        {
        
        }

        #endregion Common Methods
    }
}
