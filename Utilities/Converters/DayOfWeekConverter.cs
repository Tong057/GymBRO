using System;
using System.Globalization;

namespace GymBro.Utilities.Converters
{
    public class DayOfWeekConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DayOfWeek dayOfWeek)
            {
                if (Application.Current.Resources.TryGetValue("FullWeekNames", out object weekNamesObj))
                {
                    if (weekNamesObj is string[] weekNames)
                    {
                        switch (dayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                return weekNames[0];

                            case DayOfWeek.Tuesday:
                                return weekNames[1];

                            case DayOfWeek.Wednesday:
                                return weekNames[2];

                            case DayOfWeek.Thursday:
                                return weekNames[3];

                            case DayOfWeek.Friday:
                                return weekNames[4];

                            case DayOfWeek.Saturday:
                                return weekNames[5];

                            case DayOfWeek.Sunday:
                                return weekNames[6];
                        }
                    }
                }
            }

            return "N/A";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

