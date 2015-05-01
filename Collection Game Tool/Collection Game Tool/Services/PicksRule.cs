using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Collection_Game_Tool.Services
{
    class PicksRule : ValidationRule
    {
        private int _pick;

        public int Pick
        {
            get
            {
                return _pick;
            }
            set
            {
                _pick = value;
            }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int num=0;

            try
            {
                if (((string)value).Length > 0)
                    num = int.Parse((String)value);
            }
            catch (Exception e)
            {
                e.GetBaseException(); //This is just so warnings don't appear anymore, yeah I'm lazy
                return new ValidationResult(false, "Illegal characters");
            }

            if(Pick<num)
            {
                return new ValidationResult(false, "Please change Player Picks to support collection number of Division/PrizeLevel.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
