using System.Data.Entity.Migrations.Model;
using Culculator.Application;

namespace Culculator.Infrastructure;

public interface IIngredientContextFactory
{
    public IIngredientContext Create(string path);
}