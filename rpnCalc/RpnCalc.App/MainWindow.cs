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
        Background = Brushes.White;

        var mainPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin      = new Thickness(10),
            Spacing     = 10
        };

        var displayBorder = new Border
        {
            BorderBrush     = Brushes.Black,
            BorderThickness = new Thickness(1),
            Padding         = new Thickness(5),
            Child = new StackPanel
            {
                Spacing = 5,
                Children =
                {
                    new TextBlock
                    {
                        Text                = "Display (Top of Stack at Bottom)",
                        FontWeight          = FontWeight.Bold,
                        FontFamily          = new FontFamily("Consolas"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin              = new Thickness(0,0,0,5)
                    },
                    new TextBlock { Text = "Line 5:", FontFamily = new FontFamily("Consolas"), HorizontalAlignment = HorizontalAlignment.Center },
                    new TextBlock { Text = "Line 4:", FontFamily = new FontFamily("Consolas"), HorizontalAlignment = HorizontalAlignment.Center },
                    new TextBlock { Text = "Line 3:", FontFamily = new FontFamily("Consolas"), HorizontalAlignment = HorizontalAlignment.Center },
                    new TextBlock { Text = "Line 2:", FontFamily = new FontFamily("Consolas"), HorizontalAlignment = HorizontalAlignment.Center },
                    new TextBlock { Text = "Line 1:", FontFamily = new FontFamily("Consolas"), HorizontalAlignment = HorizontalAlignment.Center }
                }
            }
        };
        displayBorder.Height = 200;
        mainPanel.Children.Add(displayBorder);

        var grid = new Grid
        {
            RowDefinitions    = new RowDefinitions("*,*,*,*,*"),
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
            for (int col = 0; col < 4; col++)
            {
                var cell = new Border
                {
                    BorderBrush     = Brushes.Black,
                    BorderThickness = new Thickness(0.5)
                };
                var text = labels[row, col];
                if (!string.IsNullOrEmpty(text))
                    cell.Child = new Button
                    {
                        Content                   = text,
                        FontFamily                = new FontFamily("Consolas"),
                        MinHeight                 = 40,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center
                    };
                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);
                grid.Children.Add(cell);
            }

        mainPanel.Children.Add(grid);
        Content = mainPanel;
    }
}
