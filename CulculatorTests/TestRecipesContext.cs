using Microsoft.EntityFrameworkCore;

namespace Calculator.Tests
{
    public class TestRecipesContext : DbContext, IRecipesContext
    {
        public TestRecipesContext(DbContextOptions<TestRecipesContext> options)
            : base(options)
        {
        }

        public DbSet<DishEntry> RecipesDataBase { get; set; }
        public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
    }
}