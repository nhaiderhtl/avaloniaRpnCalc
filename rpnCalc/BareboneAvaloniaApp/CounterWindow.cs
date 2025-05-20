using Avalonia.Controls;

public class CounterWindow : Window
{
    private int _count = 0;
    private readonly TextBlock _label;

    public CounterWindow()
    {
        var button = new Button { Content = "Increment" };
        _label = new TextBlock { Text = "Count: 0" };

        button.Click += (_, _) => {
            _count++;
            _label.Text = $"Count: {_count}";
        };

        Content = new StackPanel
        {
            Children = { _label, button }
        };
    }
}