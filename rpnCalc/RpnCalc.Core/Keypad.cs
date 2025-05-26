using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace RpnCalc.Core;

public class Keypad : UserControl
{
    public event Action<string>? ButtonClicked;

    public Keypad()
    {
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

                bool isOp = text is "/" or "*" or "-" or "+";

                var button = new Button
                {
                    Content = text,
                    FontFamily = new FontFamily("Consolas"),
                    MinHeight = 40,
                    Padding = new Thickness(5),
                    Margin = new Thickness(5),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = isOp ? Brushes.Orange : Brushes.DarkGray,
                    BorderBrush = Brushes.DimGray,
                    BorderThickness = new Thickness(0, 0, 2, 2),
                    RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative),
                    RenderTransform = new ScaleTransform(1, 1)
                };

                button.PointerEntered += (_, __) =>
                {
                    button.Background = isOp ? Brushes.DarkOrange : Brushes.Gray;
                    button.RenderTransform = new ScaleTransform(1.1, 1.1);
                };
                button.PointerExited += (_, __) =>
                {
                    button.Background = isOp ? Brushes.Orange : Brushes.DarkGray;
                    button.RenderTransform = new ScaleTransform(1, 1);
                };

                button.Click += (_, __) => { ButtonClicked?.Invoke(text); };


                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                grid.Children.Add(button);
            }
        }

        Content = grid;
    }
}