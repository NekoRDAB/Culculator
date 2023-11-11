using Culculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class RecipesContext : DbContext
    {
        private static string _path = Path.GetFullPath(
            Path.Combine("..", "..", "..", "..", @"Culculator\RecipesDataBase.db"));

        public RecipesContext(string path)
        {
            _path = path;
        }

        public RecipesContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
        public DbSet<DishEntry> RecipesDataBase { get; set; }
    }
}