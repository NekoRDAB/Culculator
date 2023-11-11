using Culculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class RecipesContextSQLite : DbContext, IRecipesContext
    {
        private static string _path = Path.GetFullPath(
            Path.Combine("..", "..", "..", "..", @"Culculator\RecipesDataBase.db"));

        public RecipesContextSQLite(string path)
        {
            _path = path;
        }

        public RecipesContextSQLite()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
        public DbSet<DishEntry> RecipesDataBase { get; set; }
    }
}