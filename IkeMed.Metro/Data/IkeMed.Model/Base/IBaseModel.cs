using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model
{
    public interface IBaseModel
    {
        void SetEntitiesState(IkeMedContext context);
    }
}
