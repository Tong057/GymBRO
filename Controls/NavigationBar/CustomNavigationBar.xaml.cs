using System.Windows.Input;

namespace GymBro.Controls.NavigationBar;

public partial class CustomNavigationBar : ContentView
{
    public CustomNavigationBar()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomNavigationBar), "Title",
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomNavigationBar)bindable;
                control.TitleLabel.Text = (string)newValue;
            });

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomNavigationBar), Color.FromHex("#28C85A"),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (CustomNavigationBar)bindable;
                control.BackgroundBox.Color = (Color)newValue;
            });

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public static readonly BindableProperty IsBackButtonProperty =
    BindableProperty.Create(nameof(IsBackButton), typeof(bool), typeof(CustomNavigationBar), false, propertyChanged: OnIsBackButtonPropertyChanged);

    public bool IsBackButton
    {
        get => (bool)GetValue(IsBackButtonProperty);
        set => SetValue(IsBackButtonProperty, value);
    }

    private static void OnIsBackButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customNavigationBar = (CustomNavigationBar)bindable;
        var isBackButton = (bool)newValue;

        if (isBackButton)
        {
            customNavigationBar.MenuButton.IsVisible = false;
            customNavigationBar.BackButton.IsVisible = true;
        }
        else
        {
            customNavigationBar.MenuButton.IsVisible = true;
            customNavigationBar.BackButton.IsVisible = false;
        }
    }

    public static readonly BindableProperty RightButtonCommandProperty =
    BindableProperty.Create(nameof(RightButtonCommand), typeof(ICommand), typeof(CustomNavigationBar));

    public ICommand RightButtonCommand
    {
        get => (ICommand)GetValue(RightButtonCommandProperty);
        set => SetValue(RightButtonCommandProperty, value);
    }

    public static readonly BindableProperty RightButtonIconSourceProperty =
    BindableProperty.Create(nameof(RightButtonIconSource), typeof(ImageSource), typeof(CustomNavigationBar),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CustomNavigationBar)bindable;
            control.RightButton.Source = (ImageSource)newValue;
            control.RightButton.IsVisible = true;
        });

    public ImageSource RightButtonIconSource
    {
        get => (ImageSource)GetValue(RightButtonIconSourceProperty);
        set
        {
            SetValue(RightButtonIconSourceProperty, value);
        }
    }

    private void MenuButton_Clicked(object sender, System.EventArgs e)
    {
        AppShell.Current.FlyoutIsPresented = true;
    }

    private async void BackButton_Clicked(object sender, System.EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void RightButton_Clicked(object sender, System.EventArgs e)
    {
        RightButtonCommand?.Execute(null);
    }
}   