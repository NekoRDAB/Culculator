using Culculator.Domain;

namespace Culculator.Infrastructure;

public class Repository : IRepository
{
    private static IIngredientContext _ingredientsContext;
    private static IRecipesContext _recipesContext;
    private static IAddedRecipeContext _addedRecipesContext;
    public Repository(string pathToRecipes, string pathToIngredients, string pathToAddedRecipes, 
        IIngredientContextFactory ingredientContextFactory, IRecipeContextFactory recipeContextFactory,
        IAddedRecipeContextFactory addedRecipeContextFactory)
    {
        _recipesContext = recipeContextFactory.Create(pathToRecipes);
        _ingredientsContext = ingredientContextFactory.Create(pathToIngredients);
        _addedRecipesContext = addedRecipeContextFactory.Create(pathToAddedRecipes);
    }

    public Repository(IRecipesContext recipesContext, IIngredientContext ingredientContext
        , IAddedRecipeContext addedRecipeContext)
    {
        _recipesContext = recipesContext;
        _ingredientsContext = ingredientContext;
        _addedRecipesContext = addedRecipeContext;
    }

    public Repository()
    {
        _recipesContext = new RecipesContextSQLite();
        _ingredientsContext = new IngredientsContextSQLite();
        _addedRecipesContext = new AddedRecipeContext();
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
        var addedRecipes = _addedRecipesContext
            .AddedRecipesDataBase
            .Where(r => r.Category == category)
            .ToList();
        return recipes.Concat(addedRecipes).ToList();
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

    public void AddRecipeToPersonalDB(DishEntry dish)
    {
        _addedRecipesContext.AddedRecipesDataBase.Add(dish);
        _addedRecipesContext.SaveChanges();
    }
}