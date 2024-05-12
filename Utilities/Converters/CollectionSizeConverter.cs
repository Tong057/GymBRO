using System;
using System.Collections;
using System.Globalization;

namespace GymBro.Utilities.Converters
{
    public class CollectionSizeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ICollection collection)
            {
                return collection.Count;
            }

            return "N/A";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

