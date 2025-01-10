using System;
using System.Globalization;
using System.Windows.Data;

namespace Exam_janvier_2023
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return true if the value is not null; otherwise, false.
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not implemented because the conversion is one-way.
            throw new NotImplementedException();
        }
    }
}
