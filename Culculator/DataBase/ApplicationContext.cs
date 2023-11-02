using Culculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace Culculator.DataBase
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var root = new FileInfo("IngredientsDB.db");
            var fullName = root.DirectoryName;
            string newPath = fullName.Replace(@"\bin\Debug\net6.0", @"\IngredientsDB.db");
            optionsBuilder.UseSqlite($"Data Source={newPath}");
        }
        public DbSet<Ingredient> IngredientsDB { get; set; }

        // public ApplicationContext() : base("DefaultConnection")
        // {
        // }
    }
}