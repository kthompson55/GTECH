using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Collection_Game_Tool.Services
{
    class RangeRule : ValidationRule
    {
        private int _min;
        private int _max;

        public int Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters");
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false, "Please enter an age in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
