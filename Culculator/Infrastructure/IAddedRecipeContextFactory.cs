namespace Culculator.Infrastructure;

public interface IAddedRecipeContextFactory
{
    public IAddedRecipeContext Create(string path);
}