using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Routing;

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

                var func = expression.Compile();
                var attributes = new RouteValueDictionary();
                attributes["id"] = id;
                if (name.Equals(metaData.Model.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    attributes["checked"] = "checked";
                }

                var isChecked = name.Equals(metaData.Model.ToString(), StringComparison.InvariantCultureIgnoreCase) ? "checked" : "false";

                var radio = htmlHelper.RadioButtonFor(expression, value, attributes).ToHtmlString();
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
