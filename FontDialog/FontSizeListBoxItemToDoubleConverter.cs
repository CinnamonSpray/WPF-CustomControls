using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControls
{
    public class FontSizeListBoxItemToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListBoxItem item = value as ListBoxItem;
            return double.Parse(item.Content.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}