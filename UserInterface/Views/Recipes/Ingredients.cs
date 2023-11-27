using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace UserInterface.Views;

class Ingredients : StackPanel
{
    public Ingredients(Dish dish)
    {
        Margin = new Thickness(20, 0);
        HorizontalAlignment = HorizontalAlignment.Stretch;
        Children.Add(new RoundBorder(1)
        {
            Child = new IngredientsList(dish)
        });
    }
}