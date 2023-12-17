﻿using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;


public class PreDeterminedCategories : ICategories
{
    public List<Category> All { get; }
    public PreDeterminedCategories(string pathToRecipes, string pathToIngredients, 
        IRecipesContext recipesDB, DishesCollector dishesCollector)
    {
        Category.SetPaths(dishesCollector);
        All = new List<Category>
        {
            new("Завтраки"),
            new("Мясные блюда"),
            new("Гарниры"),
            new("Перекусы")
        };
    }
}