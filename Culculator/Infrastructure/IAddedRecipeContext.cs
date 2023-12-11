using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure;

public interface IAddedRecipeContext : IDisposable
{
    public DbSet<DishEntry> AddedRecipesDataBase { get; set; }
    public DbSet<IngredientEntry> AddedIngredientsDataBase { get; set; }
    public void SaveChanges();
}