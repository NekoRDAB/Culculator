using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class Application
{
    
    private IRepository _repository;
    
    public Application(IRecipesContext recipesContext, IAddedRecipeContext addedRecipeContext)
    {
        _repository = new Repository(recipesContext, addedRecipeContext);
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