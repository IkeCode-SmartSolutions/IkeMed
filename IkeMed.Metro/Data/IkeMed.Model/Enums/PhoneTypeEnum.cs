using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model.Enums
{
    public enum PhoneTypeEnum
    {
        [Display(Name = "Celular")]
        Mobile = 0,
        [Display(Name = "Residencial")]
        Residential = 1,
        [Display(Name = "Comercial")]
        Business = 2
    }
}
