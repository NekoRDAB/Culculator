using System.ComponentModel.DataAnnotations.Schema;
using Culculator.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Culculator.DataBase;

namespace Culculator.Domain
{
    [Table("IngredientsDataBase")]
    public class Ingredient
    {
        [Key]
        public int id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("conditionalUnitPrice")]
        public double ConditionalUnitPrice { get; set; }
        
        public Ingredient()
        {
        }
        
        public Ingredient(string name, double conditionalUnitPrice)
        {
            Name = name;
            ConditionalUnitPrice = conditionalUnitPrice;
        }

        // public double GetPriceFromDB(string name)
        // {
        //     using (var dbContext = new ApplicationContext())
        //     {
        //         var ingredient = dbContext.IngredientsDB.FirstOrDefault(i => i.Name == name);
        //         if (ingredient != null)
        //         {
        //             return ingredient.Price;
        //         }
        //     }
        //     return -1; // Handle the case where the ingredient is not found in the database
        // }

        // public override string ToString()
        // {
        //     return $"{IdInRecipe}.{Name}, {Amount}, {Measurement} - {Price}руб.";
        // }
        // я б добавил сюда Equals и GetHashCode но навряд ли мы будем сравнивать блюда и ингридиенты
    }
}

