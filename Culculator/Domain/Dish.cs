using System.Text;
using Culculator.Infrastructure;

namespace Domain;

public class Dish
{
    public readonly string Name;
    public readonly DishCategory Category;
    public readonly int NumberOfPortions;
    public readonly IReadOnlyList<Ingredient> Ingredients;
    public readonly string Recipe;
    public double PricePerPortion => Price / NumberOfPortions;
    public readonly double Price; 

    public Dish(
        string name, string recipe, int portions,
        DishCategory category, params Ingredient[] ingredients
        )
    {
        Name = name;
        Recipe = recipe;
        NumberOfPortions = portions;
        Category = category;
        Ingredients = new List<Ingredient>(ingredients);
        Price = Ingredients.Sum(i => i.Price);
    }

    public override string ToString() // ToString для бота
    {
        var listedIngredients = String.Join("\n", Ingredients
            .Select(i => i.ToString())
            .ToArray());
        return $"{Name}\n\n{listedIngredients}\n{Recipe}\n\n{Price}руб.\n{NumberOfPortions}\n{PricePerPortion}руб/порция";
    }
}