using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public interface IBaseModel
    {
        int ID { get; set; }
        DateTime DateIns { get; set; }
        DateTime LastUpdate { get; set; }
        bool IsActive { get; set; }

        int SaveChanges(IkeMedContext context);
    }
}
