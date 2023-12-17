using Culculator.Domain;

namespace Culculator.Infrastructure;

public class Repository : IRepository
{
    private static IRecipesContext _recipesContext;
    private static IAddedRecipeContext _addedRecipesContext;
    public Repository(IRecipesContext recipesContext, IAddedRecipeContext addedRecipeContext)
    {
        _recipesContext = recipesContext;
        _addedRecipesContext = addedRecipeContext;
    }

    public Repository()
    {
        _recipesContext = new RecipesContextSQLite();
        _addedRecipesContext = new AddedRecipeContext();
    }
    public IngredientEntry GetIngredientFromDB(string ingredientName)
    {
        var ingredient = _addedRecipesContext
            .AddedIngredientsDataBase
            .FirstOrDefault(i => i.Name == ingredientName) ?? _recipesContext
            .IngredientsDataBase
            .FirstOrDefault(i => i.Name == ingredientName);
        if(ingredient == null)
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

    private List<IngredientEntry> GetIngredients()
    {
        var ingredient = _recipesContext
            .IngredientsDataBase
            .ToList();
        var addedIngredients = _addedRecipesContext
            .AddedIngredientsDataBase
            .ToList();
        return ingredient.Concat(addedIngredients).ToList();
    }

    public List<string> GetIngredientsNames()
    {
        var allIngredients = GetIngredients();
        return allIngredients.Select(ingredient => ingredient.Name).ToList();
    }

    public void AddRecipeToPersonalDB(DishEntry dish)
    {
        _addedRecipesContext.AddedRecipesDataBase.Add(dish);
        _addedRecipesContext.SaveChanges();
    }

    public void AddIngredientToPersonalDB(IngredientEntry ingredientEntry)
    {
        _addedRecipesContext.AddedIngredientsDataBase.Add(ingredientEntry);
        _addedRecipesContext.SaveChanges();
    }
}