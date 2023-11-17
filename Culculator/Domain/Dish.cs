using System.Diagnostics;
using System.Text;
using Culculator.Infrastructure;

namespace Culculator.Domain
{
    public class Dish
    {
        public readonly IReadOnlyList<Ingredient> Ingredients;
        public readonly int NumberOfPortions;
        public readonly double Price;
        public readonly string Recipe;
        public readonly string Name;
        public readonly string Category;
        public double PricePerPortion => Price / NumberOfPortions;
        
        public Dish(DishEntry recipe, Parser parser)
        {
           
            var ingredientsList = CollectIngredients(recipe.Ingredients, parser);
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
        
        private List<Ingredient> CollectIngredients(string ingredientsFromDB, Parser parser)
        {
            var ingredientPairs = ingredientsFromDB.Split(';');
            var idCounter = 1;

            return ingredientPairs
                .Select(p => new Ingredient(idCounter++, p, parser))
                .ToList();
        }
        
        public string FormatRecipe()
        {
            var recipeSteps = Recipe.Split('.');
    
            var formattedRecipe = new StringBuilder();
    
            for (var i = 0; i < recipeSteps.Length; i++)
            {
                var stepNumber = (i + 1).ToString();
                var stepText = recipeSteps[i].Trim();
                if (!string.IsNullOrEmpty(stepText))
                {
                    formattedRecipe.AppendLine($"{stepNumber}. {stepText}");
                }
            }

            return formattedRecipe.ToString();
        }
    }
}