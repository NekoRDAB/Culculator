using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class IngredientsList : StackPanel
{
    public IngredientsList(Dish dish)
    {
        Children.Add(new Label());
        foreach (var ingredient in dish.Ingredients)
            Children.Add(new IngredientDescription(ingredient));
    }
}

class Label : TextBlock
{
    public Label()
    {
        Text = "Ингредиенты";
        Margin = new Thickness(10);
        FontSize = 30;
        FontWeight = FontWeight.SemiBold;
    }
}