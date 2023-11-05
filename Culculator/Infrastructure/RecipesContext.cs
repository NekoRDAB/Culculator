using Culculator.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure
{
    public class RecipesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var root = new FileInfo("RecipesDataBase.db");
            var fullName = root.DirectoryName;
            var newPath = fullName.Replace(@"\bin\Debug\net6.0", @"\RecipesDataBase.db");
            optionsBuilder.UseSqlite($"Data Source={newPath}");
        }
        public DbSet<DishEntry> RecipesDataBase { get; set; }
    }
}