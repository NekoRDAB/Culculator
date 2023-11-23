namespace CulculatorTests;

[TestFixture]
public class RepositoryTests
{
    [Test]
    public void GetIngredientFromDBWhenIngredientExist()
    {
        var parser = new Repository();
        var ingredientName = "Картофель";

        var ingredient = parser.GetIngredientFromDB(ingredientName);

        Assert.IsNotNull(ingredient);
        Assert.AreEqual(ingredientName, ingredient.Name);
    }

    [Test]
    public void GetIngredientFromDBWhenIngredientDoesntExist()
    {
        var parser = new Repository();
        var ingredientName = "Абракадабра";

        Assert.Throws<KeyNotFoundException>(() => parser.GetIngredientFromDB(ingredientName));
    }

    [Test]
    public void GetRecipeFromDBWhenRecipeExist()
    {
        var parser = new Repository();
        var recipeName = "Жареная картошка";

        var recipe = parser.GetRecipeFromDB(recipeName);

        Assert.IsNotNull(recipe);
        Assert.AreEqual(recipeName, recipe.Name);
    }

    [Test]
    public void GetRecipeFromDBWhenRecipeDoesntExist()
    {
        var parser = new Repository();
        var recipeName = "Абракадабра";

        Assert.Throws<KeyNotFoundException>(() => parser.GetRecipeFromDB(recipeName));
    }

    [Test]
    public void GetRecipesFromDbByCategory()
    {
        var parser = new Repository();
        var category = "TestCategory";
        using (var db = new RecipesContextSQLite())
        {
            db.RecipesDataBase.Add(new DishEntry
            {
                Name = "Recipe 1",
                Category = category, 
                Ingredients = "mock", 
                PortionsAmount = 1, 
                RecipeInfo = "mock"
            });
            db.RecipesDataBase.Add(new DishEntry
            {
                Name = "Recipe 2", 
                Category = category, 
                Ingredients = "mock", 
                PortionsAmount = 1,
                RecipeInfo = "mock"
            });
            db.RecipesDataBase.Add(new DishEntry
            {
                Name = "Recipe 3",
                Category = "Тест", 
                Ingredients = "mock",
                PortionsAmount = 1, 
                RecipeInfo = "mock"
            });
            db.SaveChanges();
        }

        var result = parser.GetRecipesFromDbByCategory(category);
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(r => r.Category == category));

        using (var dbContext = new RecipesContextSQLite())
        {
            var testRecipes = dbContext.RecipesDataBase
                .Where(r => r.Name.StartsWith("Recipe"))
                .ToList();

            dbContext.RecipesDataBase.RemoveRange(testRecipes);
            dbContext.SaveChanges();
        }
    }
}