using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishName : TextBlock
{
    public DishName(string name)
    {
        Margin = new Thickness(10);
        Text = name;
        FontSize = 30;
        MaxWidth = 420;
        FontWeight = FontWeight.SemiBold;
        TextWrapping = TextWrapping.Wrap;
        HorizontalAlignment = HorizontalAlignment.Left;
    }
}