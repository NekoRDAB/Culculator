using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;


public class PreDeterminedCategories : ICategories
{
    public List<Category> All { get; }
    public PreDeterminedCategories(string pathToRecipes, string pathToIngredients, 
        IRecipesContext recipesDB, Application application)
    {
        Category.SetPaths(pathToRecipes, pathToIngredients, application);
        All = new List<Category>
        {
            new("Завтраки"),
            new("Мясные блюда"),
            new("Гарниры"),
            new("Перекусы")
        };
    }
}