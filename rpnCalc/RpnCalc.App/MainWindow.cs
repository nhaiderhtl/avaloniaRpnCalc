using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BareboneAvaloniaApp;

public class MainWindow : Window
{
    private readonly StackDisplay _display = new();
    public MainWindow()
    {
        Title = "RPN Calculator UI";
        Width = 300;
        Height = 450;
        Background = Brushes.WhiteSmoke;

        var mainPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10),
            Spacing = 10
        };


        //mainPanel.Children.Add(displayBorder);


        mainPanel.Children.Add(_display);

        var grid = new Grid
        {
            RowDefinitions = new RowDefinitions("*,*,*,*,*"),
            ColumnDefinitions = new ColumnDefinitions("*,*,*,*")
        };

        string[,] labels =
        {
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "0", ".", "Enter", "+" },
            { "Swap", "Clear", "", "" }
        };

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                var text = labels[row, col];
                if (string.IsNullOrEmpty(text))
                    continue;

                var button = new Button
                {
                    Content = text,
                    FontFamily = new FontFamily("Consolas"),
                    MinHeight = 40,
                    Padding = new Thickness(5),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = new SolidColorBrush(Color.Parse("#A0C4FF")),
                    BorderBrush = Brushes.Transparent,
                    RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative),
                    RenderTransform = new ScaleTransform(1, 1)
                };

                button.PointerEntered += (s, e) =>
                {
                    button.Background = new SolidColorBrush(Color.Parse("#6699CC"));
                    button.RenderTransform = new ScaleTransform(1.1, 1.1);
                };
                button.PointerExited += (s, e) =>
                {
                    button.Background = new SolidColorBrush(Color.Parse("#A0C4FF"));
                    button.RenderTransform = new ScaleTransform(1, 1);
                };

                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                grid.Children.Add(button);
            }
        }

        mainPanel.Children.Add(grid);
        Content = mainPanel;
    }
}