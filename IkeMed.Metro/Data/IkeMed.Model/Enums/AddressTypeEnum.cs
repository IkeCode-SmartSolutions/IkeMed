using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model.Enums
{
    public enum AddressTypeEnum
    {
        [Display(Name = "Desconhecido")]
        Unknown = 0,
        [Display(Name = "Residencial")]
        Residential = 1,
        [Display(Name = "Comercial")]
        Commercial = 2,
        [Display(Name = "Entrega")]
        Delivery = 3,
    }
}
