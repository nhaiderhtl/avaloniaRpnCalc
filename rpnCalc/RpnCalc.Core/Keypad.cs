using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Themes.Fluent;

namespace RpnCalc.Core;

public class Keypad : UserControl
{
    public event Action<string>? ButtonClicked;

    public Keypad()
    {
        // Update theme pointer-over style
        if (Application.Current?.Styles.OfType<FluentTheme>().FirstOrDefault() is FluentTheme fluent)
        {
            fluent.Resources["ButtonBackgroundPointerOver"] = Brushes.Gray;
        }

        var mainPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10),
        };

        // Extend grid to include list operation buttons
        var grid = new Grid
        {
            RowDefinitions = new RowDefinitions("*,*,*,*,*,*"),
            ColumnDefinitions = new ColumnDefinitions("*,*,*,*"),
        };

        string[,] labels =
        {
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "0", ".", "Enter", "+" },
            { "Swap", "Clear", "[", "]" },
            { "Length", "Sum", "Avg", "Map" }
        };

        for (int row = 0; row < labels.GetLength(0); row++)
        {
            for (int col = 0; col < labels.GetLength(1); col++)
            {
                var text = labels[row, col];
                if (string.IsNullOrEmpty(text))
                    continue;

                bool isOp = text is "/" or "*" or "-" or "+" or "Enter";
                bool isListOp = text is "Length" or "Sum" or "Avg" or "Map" or "[" or "]";

                var button = new Button
                {
                    Content = text,
                    FontFamily = new FontFamily("Consolas"),
                    Height = 40,
                    Width = 60,
                    Padding = new Thickness(5),
                    Margin = new Thickness(5),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = isOp ? Brushes.Orange : isListOp ? Brushes.SteelBlue : Brushes.DarkGray,
                    Foreground = Brushes.White,
                    BorderBrush = Brushes.DimGray,
                    BorderThickness = new Thickness(0, 0, 2, 2),
                    RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative),
                    RenderTransform = new ScaleTransform(1, 1)
                };

                button.PointerEntered += (_, __) =>
                {
                    button.RenderTransform = new ScaleTransform(1.1, 1.1);
                };

                button.PointerExited += (_, __) =>
                {
                    button.RenderTransform = new ScaleTransform(1, 1);
                };

                button.Click += (_, __) => ButtonClicked?.Invoke(text);

                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                grid.Children.Add(button);
            }
        }

        mainPanel.Children.Add(grid);

        // TextBox with watermark for list input
        var listInput = new TextBox
        {
            Watermark = "Enter list of numbers (e.g. [1 2 3])",
            Margin = new Thickness(5),
            Width = 280,
            Height = 30,
            Background = Brushes.Gray,
            Foreground = Brushes.White,
        };
        
        mainPanel.Children.Add(listInput);
        Content = mainPanel;
    }
}