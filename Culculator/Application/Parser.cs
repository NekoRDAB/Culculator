using System.Runtime.InteropServices;
using System.Text;
using Culculator.Domain;
using Culculator.Infrastructure;

namespace Culculator.Application;

public static class Parser
{
    public static Ingredient ParseIngredient(int id, string ingredientEntryString, IRepository repository)
    {
        var parts = ingredientEntryString
            .Trim()
            .Split(' ');
        var name = parts[0];
        var amount = int.Parse(parts[1]); ;
        var ingredientEntry = repository.GetIngredientFromDB(name);
        name = name.Replace("_", " ");
        var price = ingredientEntry.Price;
        var measurement = ingredientEntry.MeasurementUnit;
        return new Ingredient(id, name, amount, measurement, price);
    }

    public static List<Ingredient> ParseIngredients(DishEntry dishEntry, IRepository repository)
    {
        var ingredientPairs = dishEntry.Ingredients.Split(';');
        var idCounter = 1;

        return ingredientPairs
            .Where(p => p != "" && p != " ")
            .Select(p => ParseIngredient(idCounter++, p, repository))
            .ToList();
    }

    public static List<Ingredient> ConvertIngredientsEntryToIngredients(Dictionary<IngredientEntry, int> ingredientsDict)
    {
        return ingredientsDict
            .Select(kvp 
                => new Ingredient(kvp.Key.id, kvp.Key.Name, kvp.Value, kvp.Key.MeasurementUnit, kvp.Key.Price))
            .ToList();
    }

    public static string ConvertIngredientsToString(List<Ingredient> ingredients)
    {
        var str = new StringBuilder();
        foreach (var ingredient in ingredients)
        {
            str.Append($"{ingredient.Name} {ingredient.Amount}");
            if (!ingredient.Equals(ingredients.Last()))
                str.Append("; ");
        }

        return str.ToString();
    }
}