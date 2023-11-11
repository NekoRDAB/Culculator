using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class Application
{
    
    private Parser _parser;

    private static HashSet<string> _validCategories = new()
    {
        "Завтраки",
        "Простое",
    };

    public Application(string pathToRecipes, string pathToIngredients)
    {
        _parser = new Parser(pathToRecipes, pathToIngredients);
    }

    public Application(IRecipesContext recipesContext, IIngredientContext ingredientContext)
    {
        _parser = new Parser(recipesContext, ingredientContext);
    }
    
    public List<Dish> GetAllDishesOfValidCategories()
    {
       return _parser
            .GetAllRecipesFromDB()
            .Where(d => _validCategories.Contains(d.Category))
            .Select(d => new Dish(d))
            .ToList();
    }

    public List<Dish> GetDishesByCategory(string category)
    {
        return _parser
            .GetRecipesFromDbByCategory(category)
            .Select(d => new Dish(d))
            .ToList();
    }
}