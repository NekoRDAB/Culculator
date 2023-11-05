using System.Diagnostics;
using Culculator.Infrastructure;

namespace Culculator.Domain
{
    public class Dish
    {
        public IReadOnlyList<Ingredient> Ingredients { get; private set; }
        public int NumberOfPortions { get; private set; }
        public double Price { get; private set; }
        public string Recipe { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public double PricePerPortion => Price / NumberOfPortions;
        
        public Dish(DishEntry recipe)
        {
           
            var ingredientsList = CollectIngredients(recipe.Ingredients);
            Price = ingredientsList.Sum(i => i.Price);
            Recipe = recipe.RecipeInfo;
            Name = recipe.Name;
            Category = recipe.Category;
            Ingredients = ingredientsList;
            NumberOfPortions = recipe.PortionsAmount;
        }

        public override string ToString() 
        {
            var listedIngredients = String.Join("\n", Ingredients
                .Select(i => i.ToString())
                .ToArray());
            return $"{Name}\n\n{listedIngredients}\n{Recipe}\n\n{Price}руб.\n{NumberOfPortions}\n{PricePerPortion}руб/порция";
        }
        
        private List<Ingredient> CollectIngredients(string ingredientsFromDB)
        {
            var ingredientsLists = new List<Ingredient>();
            var ingredientPairs = ingredientsFromDB.Split(';');
            var idCounter = 1;
            foreach (var pair in ingredientPairs)
            {
                var parts = pair.Trim().Split(' ');

                var ingredientName = parts[0];
                var ingredientAmount = int.Parse(parts[1]);
                var parserInstance = new Parser();
                var ingredient = parserInstance.GetIngredientFromDB(ingredientName);
                ingredientsLists.Add(new Ingredient(idCounter++, ingredientName, 
                    ingredientAmount, ingredient.MeasurementUnit, ingredient.Price));
            }

            return ingredientsLists;
        }
    }
}