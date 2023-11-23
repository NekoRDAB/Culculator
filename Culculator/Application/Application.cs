using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class Application
{
    
    private IRepository _repository;

    // private static HashSet<string> _validCategories = new()
    // {
    //     "Завтраки",
    //     "Простое",
    // };

    public Application(string pathToRecipes, string pathToIngredients)
    {
        _repository = new Repository(pathToRecipes, pathToIngredients);
    }

    public Application(IRecipesContext recipesContext, IIngredientContext ingredientContext)
    {
        _repository = new Repository(recipesContext, ingredientContext);
    }

    public Application(IRepository repository)
    {
        _repository = repository;
    }
    
    // public List<Dish> GetAllDishesOfValidCategories()
    // {
    //    return _repository
    //         .GetAllRecipesFromDB()
    //         .Where(d => _validCategories.Contains(d.Category))
    //         .Select(d => new Dish(d, _repository))
    //         .ToList();
    // }

    public List<Dish> GetDishesByCategory(string category)
    {
        return _repository
            .GetRecipesFromDbByCategory(category)
            .Select(d => new Dish(Parser.ParseIngredients(d, _repository), d.PortionsAmount,
                d.RecipeInfo, d.Name, d.Category))
            .ToList();
    }
}