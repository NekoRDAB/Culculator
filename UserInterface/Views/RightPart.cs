using Avalonia.Controls;
using Avalonia.Layout;

namespace UserInterface.Views;

class RightPart : StackPanel
{
    public RightPart(Dish dish)
    {
        Orientation = Orientation.Horizontal;
        HorizontalAlignment = HorizontalAlignment.Right;
        Children.Add(new DishShortDescription(dish));
        // Children.Add(new DishImage(dish));
    }
}