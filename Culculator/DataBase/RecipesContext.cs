using Culculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace Culculator.DataBase
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
        public DbSet<Recipe> RecipesDataBase { get; set; }
    }
}