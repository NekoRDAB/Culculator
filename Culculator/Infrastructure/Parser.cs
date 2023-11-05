namespace Culculator.Infrastructure;

public class Parser
{
    public IngredientEntry GetIngredientFromDB(string ingredientName)
    {
        using (var ingredientsDB = new IngredientsContext())
        {
            var ingredient = ingredientsDB
                .IngredientsDataBase
                .FirstOrDefault(i => i.Name == ingredientName);

            if (ingredient == null)
                throw new KeyNotFoundException($"Ингредиент с названием {ingredientName} не найден");
            return ingredient; 
        }
    }
    
    public DishEntry GetRecipeFromDB(string recipeName)
    {
        using (var recipeDB = new RecipesContext())
        {
            var recipe = recipeDB
                .RecipesDataBase
                .FirstOrDefault(i => i.Name == recipeName);

            if (recipe == null)
                throw new KeyNotFoundException($"Рецепт с названием {recipeName} не найден");
            return recipe; 
        }
    }
    
    
}