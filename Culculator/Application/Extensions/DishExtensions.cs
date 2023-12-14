using Culculator.Domain;

namespace Culculator.Application.Extensions;

public static class DishExtensions
{
    public static void RecalculateTotalPrice(this Dish dish, double newNumberOfPortions)
    {
        var coefficient = newNumberOfPortions / dish.NumberOfPortions;
        dish.Price = dish.PricePerPortion * newNumberOfPortions;
        dish.NumberOfPortions = (int)newNumberOfPortions;
        foreach (var ingredient in dish.Ingredients)
            ingredient.Amount *= coefficient;
    }
}