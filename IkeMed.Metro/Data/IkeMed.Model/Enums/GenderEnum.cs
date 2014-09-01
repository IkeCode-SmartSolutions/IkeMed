using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Desconhecido")]
        Unknown = 0,
        [Display(Name = "Masculino")]
        Male = 1,
        [Display(Name = "Feminino")]
        Female = 2
    }
}
