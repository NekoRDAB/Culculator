using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;


public class Categories
{
    public class Category
    {
        private static string _pathToRecipes;
        private static string _pathToIngredients;

        public readonly List<Dish> Dishes;
        public readonly string Name;

        internal static void SetPaths(string pathToRecipes, string pathToIngredients)
        {
            _pathToRecipes = pathToRecipes;
            _pathToIngredients = pathToIngredients;
        }

        public Category(string name, IEnumerable<Dish> dishes)
        {
            Name = name;
            Dishes = dishes.ToList();
        }

        public Category(string name)
        {
            Name = name;
            var app = new Application(new RecipesContextSQLite(_pathToRecipes),
                new IngredientsContextSQLite(_pathToIngredients));
            Dishes = app.GetDishesByCategory(name);
        }
    }

    
    public List<Category> All;
    public Categories(string pathToRecipes, string pathToIngredients)
    {
        Category.SetPaths(pathToRecipes, pathToIngredients);
        All = new()
        {
            new Category("Завтраки"),
            new Category("Горячие блюда"),
            new Category("Гарниры"),
            new Category("Перекусы")
        };
    }
}