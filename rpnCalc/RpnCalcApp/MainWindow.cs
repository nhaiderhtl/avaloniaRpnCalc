using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using RpnCalc.Core;
using RpnCalc.Exceptions;
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
        Height = 550;
        Background = Brushes.Black;
        CanResize = false;

        AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);

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

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        string? label = e.Key switch
        {
            Key.D0 or Key.NumPad0 => "0",
            Key.D1 or Key.NumPad1 => "1",
            Key.D2 or Key.NumPad2 => "2",
            Key.D3 or Key.NumPad3 => "3",
            Key.D4 or Key.NumPad4 => "4",
            Key.D5 or Key.NumPad5 => "5",
            Key.D6 or Key.NumPad6 => "6",
            Key.D7 or Key.NumPad7 => "7",
            Key.D8 or Key.NumPad8 => "8",
            Key.D9 or Key.NumPad9 => "9",

            Key.Decimal => ".",
            
            Key.Add => "+",
            Key.Subtract => "-",
            Key.Multiply => "*",
            Key.Divide => "/",
            
            Key.Enter => "Enter",
            
            Key.C => "Clear",
            Key.S => "Swap",

            _ => null
        };

        if (label != null)
        {
            OnKeypadButtonClicked(label);
            e.Handled = true;
        }
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