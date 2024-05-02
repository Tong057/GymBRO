using System;
using System.Collections.ObjectModel;

namespace GymBro.Controls.DayOfWeekPicker
{
    public partial class DayOfWeekPicker : ContentView
    {
        public static readonly BindableProperty SelectedDaysProperty = BindableProperty.Create(
             nameof(SelectedDays),
             typeof(ObservableCollection<string>),
             typeof(DayOfWeekPicker),
             defaultBindingMode: BindingMode.TwoWay
         );

        public ObservableCollection<string> SelectedDays
        {
            get => (ObservableCollection<string>)GetValue(SelectedDaysProperty);
            set => SetValue(SelectedDaysProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(DayOfWeekPicker),
            Colors.Red,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (DayOfWeekPicker)bindable;
                control.UpdateDayBoxColors();
            }
        );

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(
            nameof(UnselectedColor),
            typeof(Color),
            typeof(DayOfWeekPicker),
            Colors.Gray,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (DayOfWeekPicker)bindable;
                control.UpdateDayBoxColors();
            }
        );

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public Color UnselectedColor
        {
            get => (Color)GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        public DayOfWeekPicker()
        {
            InitializeComponent();
            BindingContext = this;

            if (SelectedDays == null)
            {
                SelectedDays = new ObservableCollection<string>();
            }

            ConnectDayBoxEvents();
        }

        private void ConnectDayBoxEvents()
        {
            var dayBoxes = new[] { DayBoxMonday, DayBoxTuesday, DayBoxWednesday, DayBoxThursday, DayBoxFriday, DayBoxSaturday, DayBoxSunday };

            foreach (var dayBox in dayBoxes)
            {
                dayBox.CheckedChanged += (s, isChecked) =>
                {
                    var dayName = dayBox.DayName;

                    if (isChecked)
                    {
                        if (!SelectedDays.Contains(dayName))
                        {
                            SelectedDays.Add(dayName);
                        }
                    }
                    else
                    {
                        if (SelectedDays.Contains(dayName))
                        {
                            SelectedDays.Remove(dayName);
                        }
                    }
                };
            }
        }

        private void UpdateDayBoxColors()
        {
            var dayBoxes = new List<DayBox> { DayBoxMonday, DayBoxTuesday, DayBoxWednesday, DayBoxThursday, DayBoxFriday, DayBoxSaturday, DayBoxSunday };

            foreach (var box in dayBoxes)
            {
                box.SelectedColor = SelectedColor;
                box.UnselectedColor = UnselectedColor;
            }
        }
    }

}
