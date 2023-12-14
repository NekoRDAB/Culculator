using Avalonia;
using Avalonia.Controls;
using Culculator.Application.Extensions;

namespace UserInterface.Views;

public class DishPortionsCount : TextBlock
{
    public DishPortionsCount(int count)
    {
        Margin = new Thickness(7, 3);
        FontSize = 29;
        Text = $"{count.FormatPortionsNumber()}";
    }
}