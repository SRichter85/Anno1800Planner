using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Anno1800Planner.Common
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public bool CollapseInsteadOfHide { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b ? Visibility.Visible : (CollapseInsteadOfHide ? Visibility.Collapsed : Visibility.Hidden);
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v != Visibility.Visible;
            return true;
        }
    }
}
