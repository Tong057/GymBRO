using System;
using System.Globalization;

namespace GymBro.Utilities.Converters
{
    public class ExerciseStringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is string name && values[1] is double weight)
            {
                return $"{name}: +{weight}kg";
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

