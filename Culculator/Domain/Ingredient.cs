using Culculator.Infrastructure;

namespace Culculator.Domain;

public struct Ingredient
{
    public readonly int IdInRecipe;
    public readonly double Amount;
    public readonly MeasurementUnit Measurement;
    public readonly string Name;
    // private readonly IIngredientRepository _repository;
    public double Price => price * Amount;
    private double price = 0;

    public Ingredient(int id, 
        string name, 
        double amount, 
        MeasurementUnit measurement)
    {
        IdInRecipe = id;
        Name = name;
        Amount = amount;
        Measurement = measurement;
        price = PostGreIngredientRepository.GetPriceFromDB(Name);
    }

    private double GetPriceFromDB() // это стоит перенести в отдельный класс репозитория
    {
        return -1; // Кто там делает БДшку напишите суда какой нибудь запрос. Желательно с EF
    }

    public override string ToString()
    {
        return $"{IdInRecipe}.{Name}, {Amount}, {Measurement} - {Price}руб.";
    }
    // я б добавил сюда Equals и GetHashCode но навряд ли мы будем сравнивать блюда и ингридиенты
}

