using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Collection_Game_Tool.Services
{
    class CreateOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ret = false;
            if (value is bool)
            {
                ret = (bool)value;

                if (ret)
                    return 1;
            }

            return 0.3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double ret = 0;
            if (value is double)
            {
                ret = (double)value;

                if (ret > 0.3)
                    return true;
            }

            return false;
        }
    }
}
