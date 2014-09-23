using IkeCode.Core.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeMed.Model.Enums
{
    public enum PersonTypeEnum
    {
        [DontParseHtml]
        None = -1,

        [EnumRouteNameAttribute("fisica")]
        [Display(Name = "Pessoa Física")]
        Natural = 0,

        [EnumRouteNameAttribute("juridica")]
        [Display(Name = "Pessoa Jurídica")]
        Legal = 1,

        [EnumRouteNameAttribute("medico")]
        [Display(Name = "Médico")]
        Doctor = 2
    }
}
