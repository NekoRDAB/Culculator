using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class DishWithImage : Dish
{
    public string PathToImage;

    public DishWithImage(IReadOnlyList<Ingredient> ingredients, int numberOfPortions, 
        string recipe, string name, string category, string pathToImage) 
        : base(ingredients, numberOfPortions, recipe, name, category)
    {
        PathToImage = pathToImage;
    }
}