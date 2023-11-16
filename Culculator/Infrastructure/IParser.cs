namespace Culculator.Infrastructure;

public interface IParser
{
    public IngredientEntry GetIngredientFromDB(string ingredientName);
    public DishEntry GetRecipeFromDB(string recipeName);

    public List<DishEntry> GetRecipesFromDbByCategory(string category);

    public List<DishEntry> GetAllRecipesFromDB();
}