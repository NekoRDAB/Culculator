[TestFixture]
public class ParserTests
{
    [Test]
    public void GetIngredientFromDBWhenIngredientExist()
    {
        var parser = new Parser();
        var ingredientName = "Яйцо";
        
        var ingredient = parser.GetIngredientFromDB(ingredientName);
        
        Assert.IsNotNull(ingredient);
        Assert.AreEqual(ingredientName, ingredient.Name);
    }

    [Test]
    public void GetIngredientFromDBWhenIngredientDoesntExist()
    {
        var parser = new Parser();
        var ingredientName = "Абракадабра";

        Assert.Throws<KeyNotFoundException>(() => parser.GetIngredientFromDB(ingredientName));
    }

    [Test]
    public void GetRecipeFromDBWhenRecipeExist()
    {
        var parser = new Parser();
        var recipeName = "Яичница";

        var recipe = parser.GetRecipeFromDB(recipeName);
        
        Assert.IsNotNull(recipe);
        Assert.AreEqual(recipeName, recipe.Name);
    }

    [Test]
    public void GetRecipeFromDBWhenRecipeDoesntExist()
    {
        var parser = new Parser();
        var recipeName = "Абракадабра";
        
        Assert.Throws<KeyNotFoundException>(() => parser.GetRecipeFromDB(recipeName));
    }

    [Test]
    public void CollectIngredients()
    {
        var parser = new Parser();
        var ingredientsFromDB = "Яйцо 3; Сосиска 2; Помидор 200";
        
        var ingredientsDict = parser.CollectIngredients(ingredientsFromDB);
        
        Assert.IsNotNull(ingredientsDict);
        Assert.AreEqual(3, ingredientsDict.Count);

    }
}
