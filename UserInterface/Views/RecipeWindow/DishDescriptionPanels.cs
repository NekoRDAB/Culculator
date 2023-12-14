using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class DishDescriptionPanels : StackPanel
{
    public DishDescriptionPanels(Dish dish, Color categoryColor)
    {
        Background = new SolidColorBrush(categoryColor);
        Children.Add(new Top(dish));
        Children.Add(new Bottom(dish));
    }
}