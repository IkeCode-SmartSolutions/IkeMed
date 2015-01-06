using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class MaxDateAttribute : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy";
        private const string DefaultErrorMessage = "'{0}' deve ser menor que {1:d}.";

        public DateTime MaxDate { get; set; }
        private bool ValidateSqlMaxValue { get; set; }

        public MaxDateAttribute(string maxDate)
            : base(DefaultErrorMessage)
        {
            this.MaxDate = ParseDate(maxDate);
        }

        public MaxDateAttribute(string maxDate, bool validateSqlMaxValue)
            : this(maxDate)
        {
            this.ValidateSqlMaxValue = validateSqlMaxValue;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return false;
            }

            DateTime dateValue = (DateTime)value;

            var isValid = dateValue <= this.MaxDate;

            if (ValidateSqlMaxValue)
                isValid = isValid && dateValue < (DateTime)SqlDateTime.MaxValue;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MaxDate);
        }

        private static DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
