
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class IngredientsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine("..", "..", "..", "..", @"Culculator\IngredientsDataBase.db");
            var fullPath = Path.GetFullPath(path);
            optionsBuilder.UseSqlite($"Data Source={fullPath}");
        }
        public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
    }
}