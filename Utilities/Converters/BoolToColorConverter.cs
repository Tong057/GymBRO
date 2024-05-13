using System.Globalization;

namespace GymBro.Utilities.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBeingDragged = (bool?)value;
            var backgroundColor = (Color)Application.Current.Resources["BackgroundColor"];
            var primaryColor = (Color)Application.Current.Resources["PrimaryColor"];

            var result = (isBeingDragged ?? false) ? backgroundColor : primaryColor;
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
