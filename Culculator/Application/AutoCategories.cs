﻿using Culculator.Infrastructure;

namespace Culculator.Application;

public class AutoCategories : ICategories
{
    public List<Category> All { get; }
    
    public AutoCategories(IRecipesContext recipesDB, DishesCollector dishesCollector)
    {
        Category.SetPaths(dishesCollector);
        All = recipesDB
            .RecipesDataBase
            .Select(d => d.Category)
            .Where(d => d != " ")
            .Distinct()
            .ToList()
            .Select(s => new Category(s))
            .ToList();
    }
}