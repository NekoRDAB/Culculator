using Culculator.Infrastructure;

namespace Culculator.Domain;

public class Ingredient
{
    private readonly int _idInRecipe;
    public double Amount;
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

    public override string ToString()
    {
        return $"{Name}, {(int)Amount} {Measurement} - {Price}руб.";
    }
}
