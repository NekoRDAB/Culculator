using Avalonia.Controls;

namespace UserInterface.Views;

class Bottom : Grid
{
    public Bottom(Dish dish)
    {
        ColumnDefinitions = new ColumnDefinitions("*,*");

        var ingredients = new Ingredients(dish);
        var recipe = new Recipe(dish);

        Children.Add(ingredients);
        Children.Add(recipe);

        SetColumn(ingredients, 0);
        SetColumn(recipe, 1);

        ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
    }
}