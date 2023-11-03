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
}