using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

    public class Category
    {
        private static string _pathToRecipes;
        private static string _pathToIngredients;
        private static DishesCollector _dishesCollector;

        public readonly List<Dish> Dishes;
        public readonly string Name;

        internal static void SetPaths(DishesCollector dishesCollector)
        {
            _dishesCollector = dishesCollector;
        }

        public Category(string name, IEnumerable<Dish> dishes)
        {
            Name = name;
            Dishes = dishes.ToList();
        }

        public Category(string name)
        {
            Name = name;
            Dishes = _dishesCollector.GetDishesByCategory(name);
        }
    }