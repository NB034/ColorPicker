using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Accessories
{
    internal class SlidersMultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) throw new ArgumentException("Converter takes 2 arguments.");
            if (!(values[0] is String first && values[1] is String second)) throw new ArgumentException("Both arguments have to be \"String\"");
            SlidersTypes type = (SlidersTypes)Enum.Parse(typeof(SlidersTypes), first);
            Byte value = System.Convert.ToByte(second);
            return new Tuple<SlidersTypes, Byte>(type, value);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { DependencyProperty.UnsetValue };
        }
    }
}
