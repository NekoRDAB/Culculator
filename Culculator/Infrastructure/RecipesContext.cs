using Culculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class RecipesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine("..", "..", "..", "..", @"Culculator\RecipesDataBase.db");
            var fullPath = Path.GetFullPath(path);
            optionsBuilder.UseSqlite($"Data Source={fullPath}");
        }
        public DbSet<DishEntry> RecipesDataBase { get; set; }
    }
}