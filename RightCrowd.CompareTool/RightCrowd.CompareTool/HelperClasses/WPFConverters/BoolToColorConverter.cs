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
    public abstract class BoolToColorConverter : IValueConverter
    {
        private SolidColorBrush _normalColor;
        private SolidColorBrush _errorColor;

        public BoolToColorConverter(SolidColorBrush normalColor, SolidColorBrush errorColor)
        {
            _normalColor = normalColor;
            _errorColor = errorColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return _errorColor;
            else
                return _normalColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(_normalColor) : false;
        }
    }
}
