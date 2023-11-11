using Culculator.Infrastructure;

namespace Culculator.Domain;

public struct Ingredient
{
    private readonly int _idInRecipe;
    public readonly double Amount;
    public readonly string Measurement;
    public readonly string Name;
    public double Price => Math.Round(_price * Amount, 2);
    private readonly double _price = 0;
    public Ingredient(int id, string name, double amount, 
        string measurement, double price)
    {
        _idInRecipe = id;
        Name = name;
        Amount = amount;
        Measurement = measurement;
        _price = price;
    }

    public Ingredient(int id, string ingredientEntryString, Parser parser)
    {
        var parts = ingredientEntryString.Trim().Split(' ');
        _idInRecipe = id;
        Name = parts[0];
        Amount = int.Parse(parts[1]); ;
        var ingredientEntry = parser.GetIngredientFromDB(Name);
        Name = Name.Replace("_", " ");
        _price = ingredientEntry.Price;
        Measurement = ingredientEntry.MeasurementUnit;
    }

    public override string ToString()
    {
        return $"{_idInRecipe}.{Name}, {Amount} {Measurement} - {Price}руб.";
    }
}
