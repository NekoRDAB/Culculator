using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Culculator.Domain
{
    [Table("IngredientsDataBase")]
    public class Ingredient
    {
        [Key]
        public int id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public double Price { get; set; }
        
        public Ingredient()
        {
        }
        
        public Ingredient(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}

