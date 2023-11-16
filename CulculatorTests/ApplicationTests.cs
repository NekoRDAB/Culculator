using Moq;
using Category = Culculator.Application.Categories.Category;

namespace CulculatorTests;

[TestFixture]
public class ApplicationTests
{
    [SetUp]
    public void Setup()
    {
        _parserMock = new Mock<IParser>();

        _application = new Application(_parserMock.Object);
    }

    private Application _application;
    private Mock<IParser> _parserMock;

    [Test]
    public void GetAllDishesOfValidCategories_NoValidCategoriesExist_ReturnsEmptyList()
    {
        var recipes = new List<DishEntry>
        {
            new() { Name = "Pasta", Category = "Dessert" },
            new() { Name = "Pizza", Category = "Soup" }
        };
        _parserMock.Setup(p => p.GetAllRecipesFromDB()).Returns(recipes);

        var result = _application.GetAllDishesOfValidCategories();

        Assert.IsEmpty(result);
    }

    [Test]
    public void GetDishesByCategory_CategoryExists_ReturnsListOfDish()
    {
        var category = "Завтраки";
        var recipes = new List<DishEntry>
        {
            new() { Name = "Pasta", Ingredients = "", Category = category },
            new() { Name = "Pizza", Ingredients = "", Category = category }
        };
        _parserMock.Setup(p => p.GetRecipesFromDbByCategory(category)).Returns(recipes);

        var result = _application.GetDishesByCategory(category);

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(d => d.Category == category));
    }

    [Test]
    public void GetDishesByCategory_CategoryDoesNotExist_ReturnsEmptyList()
    {
        var category = "Dessert";
        _parserMock.Setup(p => p.GetRecipesFromDbByCategory(category)).Returns(new List<DishEntry>());

        var result = _application.GetDishesByCategory(category);

        Assert.IsEmpty(result);
    }

    [TestFixture]
    public class DishWithImageTests
    {
        [SetUp]
        public void Setup()
        {
            _dishEntry = new DishEntry { Name = "Pasta", Ingredients = "", Category = "Завтраки" };
            _parserMock = new Parser();
            _pathToImage = "path/to/image";

            _dishWithImage = new DishWithImage(_dishEntry, _parserMock, _pathToImage);
        }

        private DishEntry _dishEntry;
        private Parser _parserMock;
        private string _pathToImage;
        private DishWithImage _dishWithImage;
        
        [Test]
        public void DishWithImage_Constructor_CallsBaseConstructor()
        {
            // Arrange
            var dishName = "Pasta";

            // Assert
            Assert.AreEqual(dishName, _dishWithImage.Name);
        }
    }

    [TestFixture]
    public class CategoryTests
    {
        [SetUp]
        public void Setup()
        {
            _pathToRecipes = "path/to/recipes";
            _pathToIngredients = "path/to/ingredients";
        }

        private string _pathToRecipes;
        private string _pathToIngredients;
        

        [Test]
        public void Categories_All_ContainsAllCategories()
        {
            // Arrange
            var expectedCategories = new List<string>
            {
                "Завтраки",
                "Горячие блюда",
                "Гарниры",
                "Перекусы"
            };
            
            var categories = new Categories(Path.GetFullPath(
                Path.Combine("..", "..", "..", "..", @"Culculator\RecipesDataBase.db")),
                Path.GetFullPath(
                    Path.Combine("..", "..", "..", "..", @"Culculator\IngredientsDataBase.db")));
            var result = categories.All.Select(c => c.Name).ToList();

            // Assert
            Assert.AreEqual(expectedCategories, result);
        }
    }
}