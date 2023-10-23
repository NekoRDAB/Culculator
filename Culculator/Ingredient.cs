namespace Culculator;

public enum MeasurementUnit
{
    Liter,
    Kilogram,
    Tbsp,
    Tsp
}

public class Ingredient
{
    public readonly int Price;
    public readonly double Amount;
    public readonly MeasurementUnit MeasurementUnit;
    public readonly string Name;
}