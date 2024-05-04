using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GymBro.Controls.DayOfWeekPicker;

public partial class WeekDayPicker : ContentView
{
    public static readonly BindableProperty WeekNamesProperty = BindableProperty.Create(nameof(WeekNames), typeof(string[]), typeof(WeekDayPicker), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (WeekDayPicker)bindable;
        string[] names = newValue as string[];

        control.UpdateWeekNaming(names);
    });

    public string[] WeekNames
    {
        get => (string[]) GetValue(WeekNamesProperty);
        set => SetValue(WeekNamesProperty, value);
    }


    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(WeekDayPicker), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (WeekDayPicker)bindable;

        control.PerformActionOnDayBox(dayBox => dayBox.SelectedColor = newValue as Color);
    });

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }


    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(WeekDayPicker), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (WeekDayPicker)bindable;

        control.PerformActionOnDayBox(dayBox => dayBox.UnselectedColor = newValue as Color);
    });

    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }


    public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(ObservableHashSet<DayOfWeek>), typeof(WeekDayPicker), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (WeekDayPicker)bindable;

        ObservableHashSet<DayOfWeek> isCheckeds = newValue as ObservableHashSet<DayOfWeek>;
        isCheckeds.CollectionChanged += (s, e) => control.UpdateIsChecked(isCheckeds);
        control.UpdateIsChecked(isCheckeds);
    });

    public ObservableHashSet<DayOfWeek> Checked
    {
        get => (ObservableHashSet<DayOfWeek>)GetValue(CheckedProperty);
        set => SetValue(CheckedProperty, value);
    }

    public WeekDayPicker()
	{
		InitializeComponent();

        //Setup events
        PerformActionOnDayBox(dayBox => dayBox.CheckedChanged += DayBoxChanged);
    }

    private void DayBoxChanged(object? sender, bool isChecked)
    {
        if (Checked == null)
            return;

        
        DayOfWeek dayOfWeek;
        if (sender == DayBoxMonday)
        {
            dayOfWeek = DayOfWeek.Monday;
        }
        else if (sender == DayBoxTuesday)
        {
            dayOfWeek = DayOfWeek.Tuesday;
        }
        else if (sender == DayBoxWednesday)
        {
            dayOfWeek = DayOfWeek.Wednesday;
        }
        else if (sender == DayBoxThursday)
        {
            dayOfWeek = DayOfWeek.Thursday;
        }
        else if (sender == DayBoxFriday)
        {
            dayOfWeek = DayOfWeek.Friday;
        }
        else if (sender == DayBoxSaturday)
        {
            dayOfWeek = DayOfWeek.Saturday;
        }
        else
        {
            dayOfWeek = DayOfWeek.Sunday;
        }


        if (isChecked)
        {
            Checked.Add(dayOfWeek);
        } else
        {
            Checked.Remove(dayOfWeek);
        }
    }

    private void UpdateWeekNaming(string[] names)
    {
        var layout = DayBoxList;
        int index = 0;

        foreach (var element in layout.Children)
        {
            if (element is DayBox)
            {
                DayBox dayBox = element as DayBox;
                dayBox.DayName = names[index];

                index++;
            }
        }
    }

    private void UpdateIsChecked(ObservableHashSet<DayOfWeek> isChecked)
    {
        DayBoxMonday.IsChecked = isChecked.Contains(DayOfWeek.Monday);
        DayBoxTuesday.IsChecked = isChecked.Contains(DayOfWeek.Tuesday);
        DayBoxWednesday.IsChecked = isChecked.Contains(DayOfWeek.Wednesday);
        DayBoxThursday.IsChecked = isChecked.Contains(DayOfWeek.Thursday);
        DayBoxFriday.IsChecked = isChecked.Contains(DayOfWeek.Friday);
        DayBoxSaturday.IsChecked = isChecked.Contains(DayOfWeek.Saturday);
        DayBoxSunday.IsChecked = isChecked.Contains(DayOfWeek.Sunday);
    }

    private void PerformActionOnDayBox(Action<DayBox> action)
    {
        var layout = DayBoxList;

        foreach (var element in layout.Children)
        {
            if (element is DayBox)
            {
                DayBox dayBox = element as DayBox;
                action?.Invoke(dayBox);
            }
        }
    }
}
