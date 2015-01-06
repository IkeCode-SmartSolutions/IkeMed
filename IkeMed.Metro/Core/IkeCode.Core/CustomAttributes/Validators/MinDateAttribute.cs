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
        private const string DefaultErrorMessage = "'{0}' deve ser maior que {1:d}.";

        public DateTime MinDate { get; set; }
        private bool ValidateSqlMinValue { get; set; }
        private string _dateFormat = "dd/MM/yyyy";
        private string DateFormat
        {
            get { return this._dateFormat; }
            set
            {
                if (value == this._dateFormat || value == null)
                    return;

                this._dateFormat = value;
            }
        }

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

        public MinDateAttribute(string minDate, string dateFormat, bool validateSqlMinValue)
            : this(minDate, validateSqlMinValue)
        {
            this.DateFormat = dateFormat;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }

            DateTime dateValue = (DateTime)value;

            var isValid = dateValue >= this.MinDate;

            if (ValidateSqlMinValue)
                isValid = isValid && dateValue >= (DateTime)SqlDateTime.MinValue;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinDate);
        }

        private DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, this.DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
