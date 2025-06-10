using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using RpnCalc.Core;

namespace BareboneAvaloniaApp;
public class MainWindow : Window
{
    private readonly ContentControl _pageHost;
    private readonly CalculatorPage _calcPage;
    private readonly FunctionsPage _functionsPage;

    public MainWindow()
    {
        Title = "RPN Calculator UI";
        Width = 300;
        Height = 500;
        Background = Brushes.Black;
        CanResize = false;

        _calcPage     = new CalculatorPage();
        _functionsPage = new FunctionsPage();

        _pageHost = new ContentControl
        {
            Content = _calcPage
        };

        var footer = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 20,
            Margin = new Thickness(0,10)
        };

        var btnCalc = new Button { Content = "Calculator" };
        btnCalc.Click += (_,_) => _pageHost.Content = _calcPage;

        var btnSettings = new Button { Content = "Draw Function" };
        btnSettings.Click += (_,_) => _pageHost.Content = _functionsPage;

        footer.Children.Add(btnCalc);
        footer.Children.Add(btnSettings);

        var dock = new DockPanel();
        DockPanel.SetDock(footer, Dock.Bottom);
        dock.Children.Add(footer);
        dock.Children.Add(_pageHost);

        Content = dock;
    }
}