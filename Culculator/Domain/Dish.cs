using Culculator.DataBase;

namespace Culculator.Domain
{
    public class Dish
    {
        public void FormRecipe(Recipe recipe)
        {
            var parserInstance = new Parser();

            var ingredientsDict = parserInstance.CollectIngredients(recipe.Ingredients);
            var recipePrice = CalculateRecipePrice(ingredientsDict);
            Console.WriteLine($"> {recipe.Name} - {recipePrice} рублей");
            foreach (var ingredientPair in ingredientsDict)
            {
                Console.WriteLine(
                    $">> {ingredientPair.Key.Name} {ingredientPair.Value} " +
                    $"- {ingredientPair.Key.Price * ingredientPair.Value}");
            }
        }

        private double CalculateRecipePrice(Dictionary<Ingredient, int> ingredientsDict)
            => ingredientsDict
                .Sum(ingredientPair => ingredientPair.Key.Price * ingredientPair.Value);
    }
}