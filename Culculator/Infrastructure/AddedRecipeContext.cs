using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure;

public class AddedRecipeContext : DbContext, IAddedRecipeContext
{
    private static string _path = Path.GetFullPath(
        Path.Combine("..", "..", "..", "..", @"Culculator\AddedRecipesDataBase.db"));

    public AddedRecipeContext(string path)
    {
        _path = path;
    }

    public AddedRecipeContext()
    {
            
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_path}");
    }
    public DbSet<DishEntry> AddedRecipesDataBase { get; set; }
    public void SaveChanges()
    {
        base.SaveChanges();
    }
}