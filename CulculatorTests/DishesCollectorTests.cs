using Moq;

namespace CulculatorTests;

[TestFixture]
public class DishesCollectorTests
{
    [SetUp]
    public void Setup()
    {
        _parserMock = new Mock<IRepository>();

        _dishesCollector = new DishesCollector(_parserMock.Object);
    }

    private DishesCollector _dishesCollector;
    private Mock<IRepository> _parserMock;

    // [Test]
    // public void GetAllDishesOfValidCategories_NoValidCategoriesExist_ReturnsEmptyList()
    // {
    //     var recipes = new List<DishEntry>
    //     {
    //         new() { Name = "Pasta", Category = "Dessert" },
    //         new() { Name = "Pizza", Category = "Soup" }
    //     };
    //     _parserMock.Setup(p => p.GetAllRecipesFromDB()).Returns(recipes);
    //
    //     var result = _application.GetAllDishesOfValidCategories();
    //
    //     Assert.IsEmpty(result);
    // }

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

        var result = _dishesCollector.GetDishesByCategory(category);

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(d => d.Category == category));
    }

    [Test]
    public void GetDishesByCategory_CategoryDoesNotExist_ReturnsEmptyList()
    {
        var category = "Dessert";
        _parserMock.Setup(p => p.GetRecipesFromDbByCategory(category)).Returns(new List<DishEntry>());

        var result = _dishesCollector.GetDishesByCategory(category);

        Assert.IsEmpty(result);
    }

    [TestFixture]
    public class DishWithImageTests
    {
        [SetUp]
        public void Setup()
        {
            _pathToImage = "path/to/image";

            _dishWithImage = new DishWithImage(new Ingredient[0], 2, "adsd",
                "Pasta", "dads", _pathToImage);
        }

        private DishEntry _dishEntry;
        private Repository _repositoryMock;
        private string _pathToImage;
        private DishWithImage _dishWithImage;

        [Test]
        public void DishWithImage_Constructor_CallsBaseConstructor()
        {
            var dishName = "Pasta";
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
    }
}