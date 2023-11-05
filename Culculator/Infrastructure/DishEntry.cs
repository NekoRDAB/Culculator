using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Culculator.Infrastructure;

namespace Culculator.Infrastructure
{
    // [Table("RecipesDataBase")]
    public class DishEntry
    {
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("category")]
        public string Category { get; set; }
        [Column("ingredients")]
        public string Ingredients { get; set; }
        [Column("portionsAmount")]
        public int PortionsAmount { get; set; }
        [Column("recipeInfo")]
        public string RecipeInfo { get; set; }

        private List<IngredientEntry> ingredients { get; set; }
        public DishEntry() { }

        public DishEntry(string name, string category, string ingredients, int portionsAmount, string recipeInfo)
        {
            Name = name;
            Category = category;
            Ingredients = ingredients;
            PortionsAmount = portionsAmount;
            RecipeInfo = recipeInfo;
        }
    }
}