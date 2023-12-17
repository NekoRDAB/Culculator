using Calculator.Tests;
using Microsoft.EntityFrameworkCore;

namespace CulculatorTests
{
    [TestFixture]
    public class RepositoryTests
    {
        private Repository _repository;
        private TestRecipesContext _testRecipesContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TestRecipesContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _testRecipesContext = new TestRecipesContext(options);
            _testRecipesContext.Database.EnsureDeleted();
            _testRecipesContext.Database.EnsureCreated();
            SeedTestData(_testRecipesContext);
            _repository = new Repository(_testRecipesContext, new AddedRecipeContext());
        }


        private void SeedTestData(TestRecipesContext dbContext)
        {
            var firstIngredient = new IngredientEntry
            {
                id = 1,
                Name = "ingredient1",
                Price = 5,
                MeasurementUnit = "Гр."
            };
            var secondIngredient = new IngredientEntry()
            {
                id = 2,
                Name = "ingredient2",
                Price = 5,
                MeasurementUnit = "Шт."
            };
            dbContext.IngredientsDataBase.Add(firstIngredient);
            dbContext.IngredientsDataBase.Add(secondIngredient);

            var firstDish = new DishEntry()
            {
                Id = 3,
                Name = "dish1",
                Category = "category1",
                Ingredients = "mock",
                PortionsAmount = 1,
                RecipeInfo = "mock"
            };
            var secondDish = new DishEntry()
            {
                Id = 4,
                Name = "dish2",
                Category = "category1",
                Ingredients = "mock",
                PortionsAmount = 1,
                RecipeInfo = "mock"
            };
            var thirdDish = new DishEntry()
            {
                Id = 5,
                Name = "dish3",
                Category = "category2",
                Ingredients = "mock",
                PortionsAmount = 1,
                RecipeInfo = "mock"
            };

            dbContext.RecipesDataBase.Add(firstDish);
            dbContext.RecipesDataBase.Add(secondDish);
            dbContext.RecipesDataBase.Add(thirdDish);

            dbContext.SaveChanges();
        }


        [Test]
        public void GetIngredientFromDBWhenIngredientExist()
        {
            var ingredientName = "ingredient1";
            var ingredient = _repository.GetIngredientFromDB(ingredientName);

            Assert.IsNotNull(ingredient);
            Assert.AreEqual(ingredientName, ingredient.Name);
        }

        [Test]
        public void GetIngredientFromDBWhenIngredientDoesntExist()
        {
            var ingredientName = "NotAnIngredient";

            Assert.Throws<KeyNotFoundException>(() => _repository.GetIngredientFromDB(ingredientName));
        }

        [Test]
        public void GetRecipeFromDBWhenRecipeExist()
        {
            var recipeName = "dish1";
            var recipe = _repository.GetRecipeFromDB(recipeName);

            Assert.IsNotNull(recipe);
            Assert.AreEqual(recipeName, recipe.Name);
        }

        [Test]
        public void GetRecipeFromDBWhenRecipeDoesntExist()
        {
            var recipeName = "NotAnRecipe";

            Assert.Throws<KeyNotFoundException>(() => _repository.GetRecipeFromDB(recipeName));
        }

        [Test]
        public void GetRecipesFromDBByCategory()
        {
            var result = _repository.GetRecipesFromDbByCategory("category1");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(r => r.Category == "category1"));
        }

        [Test]
        public void GetAllRecipesFromDB()
        {
            var result = _repository.GetAllRecipesFromDB();
            var names = new List<string> { "dish1", "dish2", "dish3" };

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            for (var i = 0; i < result.Count; i++)
                Assert.IsTrue(result[i].Name == names[i]);
        }

        [Test]
        public void GetIngredientsNames()
        {
            var result = _repository.GetIngredientsNames();
            var names = new List<string> { "ingredient1", "ingredient2" };

            Assert.IsNotNull(result);
            foreach (var t in names)
                Assert.IsTrue(result.Contains(t));
        }
    }
}