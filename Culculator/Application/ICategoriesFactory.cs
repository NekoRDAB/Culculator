namespace Culculator.Application;

public interface ICategoriesFactory
{
    public ICategories Create(string pathToRecipes, string pathToIngredients);
}