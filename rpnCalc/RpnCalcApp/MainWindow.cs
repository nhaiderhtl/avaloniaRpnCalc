using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using RpnCalc.Core;
using RpnCalc.Logic;

namespace BareboneAvaloniaApp;

public class MainWindow : Window
{
    private readonly StackDisplay _display = new();
    private readonly Keypad _keypad = new();
    private string _currentInput = "";
    private readonly RpnBaseFunctionality _functionality = new();

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
        if (label.Length == 1 && (char.IsDigit(label[0]) || label[0] == '.'))
        {
            _currentInput += label;
            _display.SetInput(_currentInput);
            return;
        }

        try
        {
            switch (label)
            {
                case "Enter":
                    if (double.TryParse(_currentInput, NumberStyles.Number, CultureInfo.InvariantCulture, out var val))
                        _functionality.Push(val);
                    _currentInput = "";
                    _display.SetInput("");
                    break;
                case "Clear":
                    _functionality.Clear();
                    _currentInput = "";
                    _display.SetInput("");
                    break;
                case "+":
                    _functionality.Add();
                    break;
                case "-":
                    _functionality.Subtract();
                    break;
                case "*":
                    _functionality.Multiply();
                    break;
                case "/":
                    _functionality.Divide();
                    break;
                case "Swap":
                    if (_functionality.Stack.Count >= 2)
                    {
                        var first = _functionality.Pop();
                        var second = _functionality.Pop();
                        _functionality.Push(first);
                        _functionality.Push(second);
                    }

                    break;
            }
        }
        catch (RpnException e)
        {
            _currentInput = e.Message;
            _display.SetInput(_currentInput);
        }

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        var items = _functionality.GetStackSnapshot();
        for (int i = 0; i < 5; i++)
        {
            int displayIndex = 4 - i;
            _display.SetLine(displayIndex, i < items.Length ? items[i].ToString(CultureInfo.InvariantCulture) : "");
        }
    }
}