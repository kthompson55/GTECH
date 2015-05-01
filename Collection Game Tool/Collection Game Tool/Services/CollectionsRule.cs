using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Collection_Game_Tool.Services
{
    class CollectionsRule : ValidationRule
    {
        private int _collection;

        public int Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
            }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int num = 0;

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

            if (Collection < num)
            {
                return new ValidationResult(false, "Please change DivisionCollection/PrizeLevelCollection to support collection number in Game Setup.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
