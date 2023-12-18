using Culculator.Infrastructure;

namespace Culculator.Application;

public class AutoCategoriesCollector : ICategories
{
    public List<Category> AllCategories { get; }
    
    public AutoCategoriesCollector(IRecipesContext recipesDB, DishesCollector dishesCollector)
    {
        Category.SetPaths(dishesCollector);
        AllCategories = recipesDB
            .RecipesDataBase
            .Select(d => d.Category)
            .Where(d => d != " ")
            .Distinct()
            .ToList()
            .Select(s => new Category(s))
            .ToList();
    }
}