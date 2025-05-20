using Avalonia.Controls;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Hello Avalonia!";
        Width = 400;
        Height = 200;

        var button = new Button { Content = "Click me" };
        button.Click += (_, _) => button.Content = "Clicked!";

        Content = new StackPanel
        {
            Children = { button }
        };
    }
}