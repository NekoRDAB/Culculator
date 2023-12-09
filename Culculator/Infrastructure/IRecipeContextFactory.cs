namespace Culculator.Infrastructure;

public interface IRecipeContextFactory
{
    public IRecipesContext Create(string path);
}