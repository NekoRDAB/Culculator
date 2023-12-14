using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

public class DishPortionsCount : TextBlock
{
    public DishPortionsCount(int count)
    {
        Margin = new Thickness(7, 3);
        FontSize = 29;
        Foreground = Brushes.Gray;
        Text = $"{count} {count.FormatPortionsNumber()}";
    }

    private string DisplayPortionsNumber(int count)
    {
        switch (count % 10)
        {
            case 1 when count % 100 != 11:
                return "порция";
            case >= 2 and <= 4 when !(count % 100 >= 12 && count % 100 <= 14):
                return "порции";
            default:
                return "порций";
        }
    }
}