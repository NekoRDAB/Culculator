using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Culculator.Infrastructure
{
    // [Table("IngredientsDataBase")]
    public class IngredientEntry
    {
        [Key]
        public int id { get; private set; }
        [Column("name")]
        public string Name { get; private set; }
        [Column("price")]
        public double Price { get; private set; }
        
        [Column("measurementUnit")]
        public string MeasurementUnit { get; private set; }
    }
}

