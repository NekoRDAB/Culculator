using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Culculator.Domain
{
    [Table("RecipesDataBase")]
    public class Recipe
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
        public double PortionsAmount { get; set; }
        [Column("recipeInfo")]
        public string RecipeInfo { get; set; }

        private List<Ingredient> ingredients { get; set; }
        public Recipe() { }

        public Recipe(string name, string category, string ingredients, double portionsAmount, string recipeInfo)
        {
            Name = name;
            Category = category;
            Ingredients = ingredients;
            PortionsAmount = portionsAmount;
            RecipeInfo = recipeInfo;
        }
    }
}