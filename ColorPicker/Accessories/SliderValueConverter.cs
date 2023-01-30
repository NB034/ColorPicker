using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Accessories
{
    internal class SliderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Type.Equals(typeof(double), value.GetType())) throw new ArgumentException("Value has to be double");
            return System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
