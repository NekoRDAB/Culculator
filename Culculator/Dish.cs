namespace Culculator;

public class Dish
{
    public readonly string Name;
    public readonly DishCategory Category;
    public readonly int NumberOfPortions;
    public IReadOnlyList<Ingredient> Ingredients = new List<Ingredient>();
    public readonly string Recipe;
    public int PricePerPortion => Price / NumberOfPortions;
    public int Price => 0; //тут будет функция вычисления цены наверно
}