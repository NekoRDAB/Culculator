﻿using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;


public class PreDeterminedCategories : ICategories
{
    public List<Category> All { get; }
    public PreDeterminedCategories(string pathToRecipes, string pathToIngredients)
    {
        Category.SetPaths(pathToRecipes, pathToIngredients);
        All = new List<Category>
        {
            new("Завтраки"),
            new("Горячие блюда"),
            new("Гарниры"),
            new("Перекусы")
        };
    }
}