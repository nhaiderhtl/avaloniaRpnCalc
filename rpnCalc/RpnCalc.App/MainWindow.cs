using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

public class MainWindow : Window
{
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

        var header = new Border
        {
            Background = Brushes.WhiteSmoke,
            BorderThickness = new Thickness(0, 0, 0, 1),
            BorderBrush = Brushes.Black,
            Margin = new Thickness(0, 0, 0, 7),
            Child = new TextBlock
            {
                Text = "Display (Top of Stack at Bottom)",
                FontWeight = FontWeight.Bold,
                FontFamily = new FontFamily("Consolas"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1)
            }
        };

        var stackPanel = new StackPanel();
        stackPanel.Children.Add(header);

        for (var line = 5; line >= 1; line--)
        {
            stackPanel.Children.Add(new TextBlock
            {
                Text = $"Line {line}:",
                FontFamily = new FontFamily("Consolas"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeight.Bold
            });
        }

        var displayBorder = new Border
        {
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            Child = stackPanel,
            Height = 150
        };

        mainPanel.Children.Add(displayBorder);

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