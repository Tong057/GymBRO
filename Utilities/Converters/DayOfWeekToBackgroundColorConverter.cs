
namespace GymBro.Utilities.Converters
{
    public class DayOfWeekToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DayOfWeek currentDayOfWeek = DateTime.Today.DayOfWeek;

            var primaryFocusColor = (Color)Application.Current.Resources["PrimaryFocusColor"];

            if (value is DayOfWeek day && day == currentDayOfWeek)
            {
                return primaryFocusColor;
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
