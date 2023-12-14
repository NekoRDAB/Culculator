using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class Bottom : Grid
{
    public Bottom(Dish dish)
    {
        ColumnDefinitions = new ColumnDefinitions("*,*");

        var ingredients = FormIngredients(dish);
        var recipe = FormRecipe(dish);

        Children.Add(ingredients);
        Children.Add(recipe);

        SetColumn(ingredients, 0);
        SetColumn(recipe, 1);

        ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
    }

    private StackPanel FormIngredients(Dish dish)
    {
        var ingredientsStackPanel = new StackPanel()
        {
            Margin = new Thickness(20, 0),
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        var border = new RoundBorder(1)
        {
            Child = new IngredientsList(dish)
        };
        ingredientsStackPanel.Children.Add(border);

        return ingredientsStackPanel;
    }

    private StackPanel FormRecipe(Dish dish)
    {
        var recipeStackPanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Margin = new Thickness(20, 0)
        };
        
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
        
        var border = new RoundBorder(1)
        {
            Child = new StackPanel
            {
                Children = { labelTextBlock, recipeTextBlock }
            }
        };
        
        recipeStackPanel.Children.Add(border);

        return recipeStackPanel;
    }
}