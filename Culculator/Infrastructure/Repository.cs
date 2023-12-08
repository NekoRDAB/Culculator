using Culculator.Domain;

namespace Culculator.Infrastructure;

public class Repository : IRepository
{
    private static IIngredientContext _ingredientsContext;
    private static IRecipesContext _recipesContext;
    public Repository(string pathToRecipes, string pathToIngredients)
    {
        _recipesContext = new RecipesContextSQLite(pathToRecipes);
        _ingredientsContext = new IngredientsContextSQLite(pathToIngredients);
    }

    public Repository(IRecipesContext recipesContext, IIngredientContext ingredientContext)
    {
        _recipesContext = recipesContext;
        _ingredientsContext = ingredientContext;
    }

    public Repository()
    {
        _recipesContext = new RecipesContextSQLite();
        _ingredientsContext = new IngredientsContextSQLite();
    }
    public IngredientEntry GetIngredientFromDB(string ingredientName)
    {
        var ingredient = _ingredientsContext
            .IngredientsDataBase
            .FirstOrDefault(i => i.Name == ingredientName);

        if (ingredient == null)
            throw new KeyNotFoundException($"Ингредиент с названием {ingredientName} не найден");
        return ingredient;
    }
    
    public DishEntry GetRecipeFromDB(string recipeName)
    {
        var recipe = _recipesContext
            .RecipesDataBase
            .FirstOrDefault(i => i.Name == recipeName);

        if (recipe == null)
            throw new KeyNotFoundException($"Рецепт с названием {recipeName} не найден");
        return recipe;
    }

    public List<DishEntry> GetRecipesFromDbByCategory(string category)
    {
        var recipes = _recipesContext
            .RecipesDataBase
            .Where(r => r.Category == category)
            .ToList();
        return recipes;
    }

    public List<DishEntry> GetAllRecipesFromDB()
    {
        var recipes = _recipesContext
            .RecipesDataBase
            .ToList();
        return recipes;
    }

    public List<IngredientEntry> GetIngredients()
    {
        var recipes = _ingredientsContext
            .IngredientsDataBase
            .ToList();
        return recipes;
    }
}