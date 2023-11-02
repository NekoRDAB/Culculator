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
        [Column("ingredient")]
        public string Ingredient { get; set; }
        [Column("conditionalUnitAmount")]
        public double ConditionalUnitAmount { get; set; }
        [Column("recipeInfo")]
        public string RecipeInfo { get; set; }

        private List<Ingredient> ingredients { get; set; }
        public Recipe() { }

        public Recipe(string name, string category, string ingredient, double conditionalUnitAmount, string recipeInfo)
        {
            Name = name;
            Category = category;
            Ingredient = ingredient;
            ConditionalUnitAmount = conditionalUnitAmount;
            RecipeInfo = recipeInfo;
        }
    }
}