using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using RpnCalc.Core;

public class MainWindow : Window
{
    private readonly StackDisplay _display = new();
    private readonly Keypad _keypad = new();
    private readonly Stack<double> _stack = new();
    private string _currentInput = "";

    public MainWindow()
    {
        Title = "RPN Calculator UI";
        Width = 300;
        Height = 470;
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

        _keypad.ButtonClicked += OnKeypadButtonClicked;
    }

    private void OnKeypadButtonClicked(string label)
    {
        //TODO: catch exception from logic
        if (label.Length == 1 && char.IsDigit(label[0]))
        {
            _currentInput += label;
            _display.SetInput(_currentInput);
            return;
        }

        switch (label)
        {
            case "Enter":
                if (double.TryParse(_currentInput, NumberStyles.Number, CultureInfo.InvariantCulture, out var val))
                    _stack.Push(val);
                _currentInput = "";
                _display.SetInput("");
                break;
            case "Clear":
                _stack.Clear();
                _currentInput = "";
                _display.SetInput("");
                break;
            case "+":
            case "-":
                if (_stack.Count >= 2)
                {
                    var b = _stack.Pop();
                    var a = _stack.Pop();
                    var result = label == "+" ? a + b : a - b;
                    _stack.Push(result);
                }

                break;
            case "Swap":
                if (_stack.Count >= 2)
                {
                    var first = _stack.Pop();
                    var second = _stack.Pop();
                    _stack.Push(first);
                    _stack.Push(second);
                }

                break;
        }

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        var items = _stack.ToArray();
        for (int i = 0; i < 5; i++)
        {
            int displayIndex = 4 - i;
            _display.SetLine(displayIndex, i < items.Length ? items[i].ToString(CultureInfo.InvariantCulture) : "");
        }
    }
}