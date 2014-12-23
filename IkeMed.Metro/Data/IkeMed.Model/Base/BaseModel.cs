using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public class BaseModel : IBaseModel
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


        public virtual int SaveChanges(IkeMedContext context)
        {
            return 0;
        }

        #endregion Common Methods
    }

    public static class ModelHelpers
    {
        public static T Find<T>(this DbSet<T> obj, int id, string[] includes = null)
            where T : class, IBaseModel, new ()
        {
            if (id > 0)
            {
                var res = obj
                    .First(i => i.ID == id);

                //if (includes != null)
                //{
                //    foreach (var include in includes)
                //    {
                //        res = obj.Include(include);
                //    }
                //}

                return res;
            }

            return new T();
        }
    }
}
