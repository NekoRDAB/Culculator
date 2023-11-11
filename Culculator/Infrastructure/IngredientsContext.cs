
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class IngredientsContextSQLite : DbContext, IIngredientContext
    {
        private static string _path = Path.GetFullPath(
            Path.Combine("..", "..", "..", "..", @"Culculator\IngredientsDataBase.db"));

        public IngredientsContextSQLite(string path)
        {
            _path = path;
        }

        public IngredientsContextSQLite()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
        public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
    }
}