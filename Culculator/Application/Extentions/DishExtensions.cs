using Culculator.Domain;

namespace Culculator.Application.Extentions;

public static class DishExtensions
{
    public static void RecalculateTotalPrice(this Dish dish, int newNumberOfPortions)
    {
        var coefficient = newNumberOfPortions / dish.NumberOfPortions;
        dish.Price = dish.PricePerPortion * newNumberOfPortions;
        dish.NumberOfPortions = newNumberOfPortions;
        foreach (var ingredient in dish.Ingredients)
            ingredient.Amount *= coefficient;
    }
}