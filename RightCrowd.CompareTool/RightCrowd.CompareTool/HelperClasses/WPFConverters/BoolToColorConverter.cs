using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RightCrowd.CompareTool.HelperClasses.WPFConverters
{
    /// <summary>
    /// Converts a boolean to the specified colors. If the boolean is false, it returns the normal color, 
    /// if the boolean is true, it returns the error color.
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        public SolidColorBrush TrueValue { get; set; }
        public SolidColorBrush FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return TrueValue;
            else
                return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }
}
