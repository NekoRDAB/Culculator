namespace Culculator.Domain;

public struct Ingredient
{
    public readonly int IdInRecipe;
    public readonly double Amount;
    public readonly string Measurement;
    public readonly string Name;
    public double Price => price * Amount;
    private double price = 0;
    public Ingredient(int id, string name, double amount, 
        string measurement, double price)
    {
        IdInRecipe = id;
        Name = name;
        Amount = amount;
        Measurement = measurement;
        this.price = price;
    }

    public override string ToString()
    {
        return $"{IdInRecipe}.{Name}, {Amount} {Measurement} - {Price}руб.";
    }
}
