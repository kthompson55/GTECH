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
        private int _collection;

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
            throw new NotImplementedException();
        }
    }
}
