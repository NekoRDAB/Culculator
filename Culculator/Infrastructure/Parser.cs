namespace Culculator.Infrastructure;

public class Parser
{
    private static IIngredientContext _ingredientsContext;
    private static IRecipesContext _recipesContext;
    public Parser(string pathToRecipes, string pathToIngredients)
    {
        _recipesContext = new RecipesContextSQLite(pathToRecipes);
        _ingredientsContext = new IngredientsContextSQLite(pathToIngredients);
    }

    public Parser(IRecipesContext recipesContext, IIngredientContext ingredientContext)
    {
        _recipesContext = recipesContext;
        _ingredientsContext = ingredientContext;
    }

    public Parser()
    {
        _recipesContext = new RecipesContextSQLite();
        _ingredientsContext = new IngredientsContextSQLite();
    }
    public IngredientEntry GetIngredientFromDB(string ingredientName)
    {
        using var ingredientsDB =  _ingredientsContext;
        var ingredient = ingredientsDB
            .IngredientsDataBase
            .FirstOrDefault(i => i.Name == ingredientName);

        if (ingredient == null)
            throw new KeyNotFoundException($"Ингредиент с названием {ingredientName} не найден");
        return ingredient;
    }
    
    public DishEntry GetRecipeFromDB(string recipeName)
    {
        using var recipeDB = _recipesContext;
        var recipe = recipeDB
            .RecipesDataBase
            .FirstOrDefault(i => i.Name == recipeName);

        if (recipe == null)
            throw new KeyNotFoundException($"Рецепт с названием {recipeName} не найден");
        return recipe;
    }

    public List<DishEntry> GetRecipesFromDbByCategory(string category)
    {
        using var recipeDb = _recipesContext;
        var recipes = recipeDb
            .RecipesDataBase
            .Where(r => r.Category == category)
            .ToList();
        return recipes;
    }

    public List<DishEntry> GetAllRecipesFromDB()
    {
        using var recipeDb = _recipesContext;
        var recipes = recipeDb
            .RecipesDataBase
            .ToList();
        return recipes;
    }
}