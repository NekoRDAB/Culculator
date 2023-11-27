using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class Title : StackPanel
{
    public Title()
    {
        Children.Add(new TextBlock
        {
            Text = "Culculator", FontSize = 80,
            TextAlignment = TextAlignment.Center
        });
        Spacing = -7;
        Children.Add(new TextBlock
        {
            Text = "Culinary Calculator", FontSize = 25, Foreground = Brushes.LightGray,
            TextAlignment = TextAlignment.Center
        });
        Margin = new Thickness(10);
    }
}