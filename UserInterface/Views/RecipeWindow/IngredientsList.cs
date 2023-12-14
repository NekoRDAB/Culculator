using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class IngredientsList : StackPanel
{
    public IngredientsList(Dish dish)
    {
        var label = new TextBlock()
        {
            Text = "Ингредиенты",
            Margin = new Thickness(10),
            FontSize = 30,
            FontWeight = FontWeight.SemiBold
        };
        Children.Add(label);
        foreach (var ingredient in dish.Ingredients)
            Children.Add(FormIngredient(ingredient));
    }

    private StackPanel FormIngredient(Ingredient ingredient)
    {
        var checkBox = new CheckBox();
        var ingredientTextBlock = new TextBlock
        {
            Text = $"• {ingredient}",
            FontSize = 20,
            TextWrapping = TextWrapping.Wrap
        };
        var ingredientPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(5),
        };
        ingredientPanel.Children.Add(checkBox);
        ingredientPanel.Children.Add(ingredientTextBlock);
        return ingredientPanel;
    }
}