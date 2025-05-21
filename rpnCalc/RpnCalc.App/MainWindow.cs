using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BareboneAvaloniaApp;

public class MainWindow : Window
{
    private readonly StackDisplay _display = new();
    private readonly Keypad _keypad = new();

    public MainWindow()
    {
        Title = "RPN Calculator UI";
        Width = 300;
        Height = 450;
        Background = Brushes.Black;
        CanResize = false;

        var mainPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10),
            Spacing = 10
        };

        mainPanel.Children.Add(_display);
        mainPanel.Children.Add(_keypad);
        Content = mainPanel;
    }
}