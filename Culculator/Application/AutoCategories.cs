using Culculator.Infrastructure;

namespace Culculator.Application;

public class AutoCategories : ICategories
{
    public List<Category> All { get; }
    
    public AutoCategories(string pathToRecipes, string pathToIngredients, 
        IRecipesContext recipesDB, IApplicationFactory applicationFactory)
    {
        var application = applicationFactory.Create(pathToRecipes, pathToIngredients);
        Category.SetPaths(pathToRecipes, pathToIngredients, application);
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