using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace RpnCalc.Core;

public class StackDisplay : UserControl
{
    private readonly TextBlock[] _lines;
    private TextBlock _inputLine;

    public StackDisplay()
    {
        _lines = new TextBlock[5];

        var header = new Border
        {
            Background = Brushes.DimGray,
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

        var panel = new StackPanel
        {
            Spacing = 5,
            Background = Brushes.DimGray,
        };
        panel.Children.Add(header);

        for (int i = 5; i > 0; i--)
        {
            var lineBlock = new TextBlock
            {
                Text = $"Line {i}:",
                FontFamily = new FontFamily("Consolas"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeight.Bold
            };
            _lines[^i] = lineBlock;
            panel.Children.Add(lineBlock);
        }

        var inputLine = new TextBlock
        {
            Text = $"Input: ",
            FontFamily = new FontFamily("Consolas"),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10),
            FontWeight = FontWeight.Bold,
        };
        panel.Children.Add(inputLine);
        _inputLine = inputLine;

        Content = new Border
        {
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            Child = panel,
            Height = 200
        };
    }

    public void SetInput(string input)
    {
        _inputLine.Text = "Input: " + input;
    }
}