using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IkeCode.Core.CustomAttributes.Helpers
{
    public static class EnumDisplayAttributeHelper
    {
        public static string GetDisplayName<T>(this T value)
            where T : struct
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes<DisplayAttribute>(false) as DisplayAttribute[];
            
            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}
