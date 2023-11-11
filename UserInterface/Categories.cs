using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Culculator.Domain;
using static System.Linq.Enumerable;

namespace UserInterface;

public class DishWithImage : Dish
{
    public string Image;

    public DishWithImage(DishEntry dishEntry, string image) : base(dishEntry)
    {
        Image = image;
    }
}

public class Category
{
    private static string _pathToRecipes;
    private static string _pathToIngredients;
    static Category()
    {
        var dir = Environment.CurrentDirectory;
        _pathToRecipes =dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db");
        _pathToIngredients = dir.Replace("UserInterface", "Culculator\\IngredientsDataBase.db");
    }
    public readonly string Name;

    public readonly List<Dish> Dishes;

    public Category(string name, IEnumerable<Dish> dishes)
    {
        Name = name;
        Dishes = dishes.ToList();
    }

    public Category(string name)
    {
        Name = name;
        var app = new Application(new RecipesContextSQLite(_pathToRecipes), 
            new IngredientsContextSQLite(_pathToIngredients));
        Dishes = app.GetDishesByCategory(name);
    }
}

public static class Categories
{
    public static List<Category> All = new()
    {
        new("Завтраки"),
        new("Горячие блюда"),
        new("Гарниры"),
        new("Перекусы")
    };
    // public static List<Category> All = new()
    // {
    //     new("Завтраки", 
    //     
    //         Repeat(new DishWithImage()
    //         {
    //             Name = "Овсянка",
    //             Category = "Завтраки",
    //             Recipe = "рецепт",
    //             Price = 43,
    //             NumberOfPortions = 1,
    //             Ingredients = new Ingredient[]
    //             {
    //             },
    //             Image = "Assets\\ovsyanka.jpg",
    //         }, 10)
    //     ),
    //     new("Мясные блюда", new Dish[]
    //     {
    //         new DishWithImage()
    //         {
    //             Name = "Куриные тефтели",
    //             Category = "Мясные блюда",
    //             Recipe = "рецепт",
    //             Price = 100,
    //             NumberOfPortions = 6,
    //             Ingredients = new[]
    //             {
    //                 new Ingredient(1, "фарш", 0.5, "кг", 200),
    //             },
    //             Image = "Assets\\kurinye-tefteli.jpg",
    //         },
    //         new()
    //         {
    //             Name = "Паровые котлеты",
    //             Category = "Мясные блюда",
    //             Recipe = "рецепт",
    //             Price = 107,
    //             NumberOfPortions = 6,
    //             Ingredients = new[]
    //             {
    //                 new Ingredient(1, "фарш", 0.5, "кг", 200),
    //                 new Ingredient(1, "яйцо", 1, "шт", 7)
    //             }
    //         }
    //     }),
    //     new("Гарниры", new Dish[] { }),
    //     new("Перекусы", new Dish[] { })
    // };
}