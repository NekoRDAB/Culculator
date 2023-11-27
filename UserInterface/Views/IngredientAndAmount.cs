using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class IngredientAndAmount : StackPanel
{
    public IngredientAndAmount(Ingredient ingredient)
    {
        Orientation = Orientation.Horizontal;
        Margin = new Thickness(5);

        Children.Add(new CheckBox());

        var ingredientTextBlock = new TextBlock
        {
            Text = $"• {ingredient}",
            FontSize = 20,
            TextWrapping = TextWrapping.Wrap
        };
        Children.Add(ingredientTextBlock);
    }
}