
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class IngredientsContext : DbContext
    {
        private static string _path = Path.GetFullPath(
            Path.Combine("..", "..", "..", "..", @"Culculator\IngredientsDataBase.db"));

        public IngredientsContext(string path)
        {
            _path = path;
        }

        public IngredientsContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
        public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
    }
}