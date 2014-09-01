using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class IkeCodeHtmlHelpers
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(
                this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var sb = new StringBuilder();
            var enumType = metaData.ModelType;
            foreach (var field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                var value = (int)field.GetValue(null);
                var name = Enum.GetName(enumType, value);
                var label = name;
                foreach (DisplayAttribute currAttr in field.GetCustomAttributes(typeof(DisplayAttribute), true))
                {
                    label = currAttr.Name;
                    break;
                }

                var id = string.Format(
                    "{0}_{1}_{2}",
                    metaData.ContainerType.Name,
                    metaData.PropertyName,
                    name
                );
                var radio = htmlHelper.RadioButtonFor(expression, value, new { id = id }).ToHtmlString();
                sb.AppendFormat(
                    "<label class=\"inline-block\" style=\"margin-right: 5px;\">{0} <span class=\"check\"></span> {1}</label>",                    
                    radio,
                    HttpUtility.HtmlEncode(label)
                );
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
