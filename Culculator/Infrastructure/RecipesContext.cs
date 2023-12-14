using Culculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class RecipesContextSQLite : DbContext, IRecipesContext
    {
        private static string _path = Path.GetFullPath(
            Path.Combine("..", "..", "..", "..", @"Culculator\RecipesDataBase.db"));

        public RecipesContextSQLite(RecipeContextPathProvider provider)
        {
            _path = provider.PathToDefaultDB;
        }

        public RecipesContextSQLite()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
        public DbSet<DishEntry> RecipesDataBase { get; set; }
        public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
    }
}