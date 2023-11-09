using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public class Application
{
    private static Parser _parser = new();

    private static HashSet<string> _validCategories = new()
    {
        "Горячие блюда",
        "Гарниры",
    };
    
    public static List<Dish> GetAllDishesOfValidCategories()
    {
       return _parser
            .GetAllRecipesFromDB()
            .Where(d => _validCategories.Contains(d.Category))
            .Select(d => new Dish(d))
            .ToList();
    }

    public static List<Dish> GetDishesByCategory(string category)
    {
        return _parser
            .GetRecipesFromDbByCategory(category)
            .Select(d => new Dish(d))
            .ToList();
    }
}