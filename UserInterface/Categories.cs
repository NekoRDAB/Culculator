using System.Collections.Generic;
using System.Linq;
using Culculator.Domain;

namespace UserInterface;

public class Category
{
    public readonly string Name;

    public readonly List<Dish> Dishes;

    public Category(string name, IEnumerable<Dish> dishes)
    {
        Name = name;
        Dishes = dishes.ToList();
    }
}

public static class Categories
{
    public static List<Category> All = new()
    {
        new("Завтраки", new Dish[]
        {
            new()
            {
                Name = "Овсянка",
                Category = "Завтраки",
                Recipe = "рецепт",
                Price = 43,
                NumberOfPortions = 1,
                Ingredients = new Ingredient[]
                {
                }
            },
        }),
        new("Мясные блюда", new Dish[]
        {
            new()
            {
                Name = "Куриные тефтели",
                Category = "Мясные блюда",
                Recipe = "рецепт",
                Price = 100,
                NumberOfPortions = 6,
                Ingredients = new[]
                {
                    new Ingredient(1, "фарш", 0.5, "кг", 200)
                }
            },
            new()
            {
                Name = "Паровые котлеты",
                Category = "Мясные блюда",
                Recipe = "рецепт",
                Price = 107,
                NumberOfPortions = 6,
                Ingredients = new[]
                {
                    new Ingredient(1, "фарш", 0.5, "кг", 200),
                    new Ingredient(1, "яйцо", 1, "шт", 7)
                }
            }
        }),
        new("Гарниры", new Dish[] { }),
        new("Перекусы", new Dish[] { })
    };
}

public class Dish
{
    public IReadOnlyList<Ingredient> Ingredients;
    public int NumberOfPortions;
    public double Price;
    public string Recipe;
    public string Name;
    public string Category;
    public double PricePerPortion => Price / NumberOfPortions;
}