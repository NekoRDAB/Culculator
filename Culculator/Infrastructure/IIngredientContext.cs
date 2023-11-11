using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure;

public interface IIngredientContext : IDisposable
{
    public DbSet<IngredientEntry> IngredientsDataBase { get; set; }
}