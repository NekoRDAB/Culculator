using System.ComponentModel.DataAnnotations.Schema;
using Culculator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Culculator.Domain
{
    [Table("IngredientsDB")]
    public class Ingredient
    {
        [Key]
        public int IdInRecipe { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("measurement")]
        public string Measurement { get; set; }
        [Column("name")]
        public string Name { get; set; }

        // private readonly IIngredientRepository _repository;
        [Column("price")]
        public double Price { get; set; }
        // {
        //     get => price * Amount;
        //     set => price = value;
        // }
        // private double price = 0;

        // Пустой конструктор нужен для базы данных        
        public Ingredient()
        {
        }
        
        public Ingredient(int amount, string measurement, string name, double price)
        {
            Amount = amount;
            Measurement = measurement;
            Name = name;
            Price = price;
            // price = PostGreIngredientRepository.GetPriceFromDB(Name);
        }

        private double GetPriceFromDB() // это стоит перенести в отдельный класс репозитория
        {
            return -1; // Кто там делает БДшку напишите суда какой нибудь запрос. Желательно с EF
        }

        // public override string ToString()
        // {
        //     return $"{IdInRecipe}.{Name}, {Amount}, {Measurement} - {Price}руб.";
        // }
        // я б добавил сюда Equals и GetHashCode но навряд ли мы будем сравнивать блюда и ингридиенты
    }
}

