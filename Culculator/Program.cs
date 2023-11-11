using Culculator.Domain;
using Culculator.Infrastructure;
namespace Culculator
{
    public class Program
    {
        public static void Main()
        {
            var parserInstance = new Parser();
            Console.WriteLine(Environment.CurrentDirectory);
            var recipes = parserInstance.GetRecipesFromDbByCategory("Горячие блюда");
            foreach (var recipe in recipes)
            {
                var dish = new Dish(recipe, parserInstance);
                Console.WriteLine(dish);
            }
        }
    }
}

