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
        if (double.TryParse(label, NumberStyles.None, CultureInfo.InvariantCulture, out _))
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
                if (_stack.Count >= 2)
                {
                    var b = _stack.Pop();
                    var a = _stack.Pop();
                    _stack.Push(a + b);
                }

                break;

            case "-":
                if (_stack.Count >= 2)
                {
                    var b = _stack.Pop();
                    var a = _stack.Pop();
                    _stack.Push(a - b);
                }

                break;

            case "Swap":
                if (_stack.Count >= 2)
                {
                    var top = _stack.Pop();
                    var second = _stack.Pop();
                    _stack.Push(top);
                    _stack.Push(second);
                }

                break;
        }

        RefreshDisplayLines();
    }

    private void RefreshDisplayLines()
    {
        var items = _stack.ToArray();
        for (int i = 0; i < 5; i++)
        {
            if (i < items.Length)
                _display.SetLine(i, items[i].ToString(CultureInfo.InvariantCulture));
            else
                _display.SetLine(i, "");
        }
    }
}