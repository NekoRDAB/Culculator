using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Culculator.Infrastructure
{
    // [Table("IngredientsDataBase")]
    public class IngredientEntry
    {
        [Key]
        public int id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public double Price { get; set; }
        
        [Column("measurementUnit")]
        public string MeasurementUnit { get; set; }
    }
}

