﻿namespace Infrastructure;

public struct Ingredient
{
    public readonly int IdInRecipe;
    public readonly double Amount;
    public readonly MeasurementUnit Measurement;
    public readonly string Name;
    public double Price => price * Amount;
    private double price;

    public Ingredient(int id, string name, double amount, MeasurementUnit measurement)
    {
        IdInRecipe = id;
        Name = name;
        Amount = amount;
        Measurement = measurement;
        price = -1; // структуры немного корявые но учитывая то, что ингредиентов список оно того стоит
        price = GetPriceFromDB();
    }

    private double GetPriceFromDB()
    {
        return -1; // Кто там делает БДшку напишите суда какой нибудь запрос. Желательно с EF
    }

    public override string ToString()
    {
        return $"{IdInRecipe}.{Name}, {Amount}, {Measurement} - {Price}руб.";
    }
}
    
    