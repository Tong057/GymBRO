using CommunityToolkit.Mvvm.ComponentModel;

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


    public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool[]), typeof(WeekDayPicker), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (WeekDayPicker)bindable;

        bool[] isCheckeds = newValue as bool[];
        control.UpdateIsChecked(isCheckeds);
    });

    public bool[] Checked
    {
        get => (bool[])GetValue(CheckedProperty);
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

        var layout = DayBoxList;

        int index = 0;
        foreach (var element in layout.Children)
        {
            if (element is DayBox)
            {
                DayBox dayBox = element as DayBox;

                if(dayBox == sender)
                {
                    Checked[index] = isChecked;
                    return;
                }

                index++;
            }
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

    private void UpdateIsChecked(bool[] isChecked)
    {
        var layout = DayBoxList;
        int index = 0;

        foreach (var element in layout.Children)
        {
            if (element is DayBox)
            {
                DayBox dayBox = element as DayBox;
                dayBox.IsChecked = isChecked[index];

                index++;
            }
        }
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
