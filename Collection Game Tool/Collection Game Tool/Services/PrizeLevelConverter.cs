using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Collection_Game_Tool.Services
{
    public class PrizeLevelConverter : IValueConverter
    {
        List<String> levels = new List<String>()
            {
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T"
            };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ret=0;
            if (value is int)
            {
                ret = (int)value;
                return levels[ret - 1];
            }

            return ret.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String text = (string)value;
            int ret;
            if (!int.TryParse(text, out ret))
            {
                return 0;
            }

            return ret;
        }
    }
}
