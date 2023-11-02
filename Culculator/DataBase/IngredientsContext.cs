using Culculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace Culculator.DataBase
{
    public class IngredientsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var root = new FileInfo("IngredientsDataBase.db");
            var fullName = root.DirectoryName;
            var newPath = fullName.Replace(@"\bin\Debug\net6.0", @"\IngredientsDataBase.db");
            optionsBuilder.UseSqlite($"Data Source={newPath}");
        }
        public DbSet<Ingredient> IngredientsDataBase { get; set; }
    }
}