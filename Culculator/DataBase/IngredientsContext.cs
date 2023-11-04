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
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\79521\Desktop\Culculator\Culculator\Culculator\IngredientsDataBase.db");
        }
        public DbSet<Ingredient> IngredientsDataBase { get; set; }
    }
}