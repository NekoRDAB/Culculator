using System;
using Culculator.DataBase;
using Culculator.Domain;


public class Parser
{
    public Ingredient GetIngredientFromDB(string ingredientName)
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
    
    public Recipe GetRecipeFromDB(string recipeName)
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
    
    public Dictionary<Ingredient, int> CollectIngredients(string ingredientsFromDB)
    {
        var ingredientsDictionary = new Dictionary<Ingredient, int>();
        var ingredientPairs = ingredientsFromDB.Split(';');

        foreach (var pair in ingredientPairs)
        {
            var parts = pair.Trim().Split(' ');

            var ingredientName = parts[0];
            var ingredientAmount = int.Parse(parts[1]);

            var ingredient = GetIngredientFromDB(ingredientName);
            ingredientsDictionary.Add(ingredient, ingredientAmount);
        }

        return ingredientsDictionary;
    }
}