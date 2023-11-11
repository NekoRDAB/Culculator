using Microsoft.EntityFrameworkCore;

namespace Culculator.Infrastructure;

public interface IRecipesContext : IDisposable
{
    public DbSet<DishEntry> RecipesDataBase { get; set; }
}