﻿using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class Application
{
    
    private IRepository _repository;
    
    public Application(string pathToRecipes, string pathToIngredients, IRepositoryFactory factory)
    {
        _repository = factory.Create(pathToRecipes, pathToIngredients);
    }

    public Application(IRecipesContext recipesContext, IIngredientContext ingredientContext)
    {
        _repository = new Repository(recipesContext, ingredientContext);
    }

    public Application(IRepository repository)
    {
        _repository = repository;
    }

    public List<Dish> GetDishesByCategory(string category)
    {
        return _repository
            .GetRecipesFromDbByCategory(category)
            .Select(d => new Dish(Parser.ParseIngredients(d, _repository), d.PortionsAmount,
                d.RecipeInfo, d.Name, d.Category))
            .ToList();
    }
}