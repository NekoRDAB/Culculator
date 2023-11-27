using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class Recipe : StackPanel
{
    public Recipe(Dish dish)
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        Margin = new Thickness(20, 0);

        var labelTextBlock = new TextBlock
        {
            FontWeight = FontWeight.SemiBold,
            Text = "Рецепт",
            FontSize = 30,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(10),
        };

        var recipeTextBlock = new TextBlock
        {
            Text = dish.FormatRecipe(),
            FontSize = 20,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(7),
        };

        Children.Add(new RoundBorder(1)
        {
            Child = new StackPanel
            {
                Children = { labelTextBlock, recipeTextBlock }
            }
        });
    }
}