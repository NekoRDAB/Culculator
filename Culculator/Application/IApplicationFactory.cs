namespace Culculator.Application;

public interface IApplicationFactory
{
    public Application Create(string pathToRecipes, string pathToAddedRecipes);
}