using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class MinDateAttribute : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy";
        private const string DefaultErrorMessage = "'{0}' deve ser maior que {1:d}.";

        public DateTime MinDate { get; set; }
        private bool ValidateSqlMinValue { get; set; }

        public MinDateAttribute(string minDate)
            : base(DefaultErrorMessage)
        {
            this.MinDate = ParseDate(minDate);
        }

        public MinDateAttribute(string minDate, bool validateSqlMinValue)
            : this(minDate)
        {
            this.ValidateSqlMinValue = validateSqlMinValue;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }

            DateTime dateValue = (DateTime)value;

            var isValid = dateValue <= this.MinDate;

            if (ValidateSqlMinValue)
                isValid = isValid && dateValue > (DateTime)SqlDateTime.MinValue;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinDate);
        }

        private static DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
