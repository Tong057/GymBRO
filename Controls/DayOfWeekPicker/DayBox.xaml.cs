using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Shapes;

namespace GymBro.Controls.DayOfWeekPicker;

public partial class DayBox : ContentView
{
    public event EventHandler<bool> CheckedChanged;

    public static readonly BindableProperty DayNameProperty = BindableProperty.Create(nameof(DayName), typeof(string), typeof(DayBox), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (DayBox)bindable;

        control.Day.Text = newValue as string;
    });

    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DayBox), false, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (DayBox)bindable;

        control.IsChecked = (bool)newValue;
        control.UpdateFillColor();
    });

    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(DayBox), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (DayBox)bindable;

        control.UpdateFillColor();
    });

    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(DayBox), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (DayBox)bindable;

        control.UpdateFillColor();
    });

    public DayBox()
    {
        InitializeComponent();

        SelectedColor = Colors.Red;
        UnselectedColor = Colors.Gray;
    }

    public string DayName
    {
        get => GetValue(DayNameProperty) as string;
        set => SetValue(DayNameProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public Color UnselectedColor
    {
        get => (Color) GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }

    public Color SelectedColor
    {
        get => (Color) GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }


    private void UpdateFillColor()
    {
        Color color = IsChecked ? SelectedColor : UnselectedColor;
        ChangeBackgroundCircleColor(color);
    }

    private void ChangeBackgroundCircleColor(Color color)
    {
        BackgroundCircle.Stroke= color;

        Animation animation;
        if (IsChecked)
        {
            animation = new Animation(v => BackgroundCircle.StrokeThickness = v, 4, 5.5, Easing.Linear);
        } else
        {
            animation = new Animation(v => BackgroundCircle.StrokeThickness = v, 5.5, 4, Easing.Linear);
        }
        
        BackgroundCircle.Animate("StrokeAnimation", animation, 32, 200);
    }

    private void OnTapped(object sender, TappedEventArgs e)
    {
        IsChecked = !IsChecked;
        UpdateFillColor();

        CheckedChanged?.Invoke(this, IsChecked);
    }
}
