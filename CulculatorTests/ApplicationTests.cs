using Moq;
using Category = Culculator.Application.Categories.Category;

namespace CulculatorTests;

[TestFixture]
public class ApplicationTests
{
    [SetUp]
    public void Setup()
    {
        _parserMock = new Mock<IRepository>();

        _application = new Application(_parserMock.Object);
    }

    private Application _application;
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
        

        [Test]
        public void Categories_All_ContainsAllCategories()
        {
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

            Assert.AreEqual(expectedCategories, result);
        }
    }
    
    [TestFixture]
    public class FormatingExtentionsTests
    {
        [TestCase(1, ExpectedResult = "1 порция")]
        [TestCase(2, ExpectedResult = "2 порции")]
        [TestCase(5, ExpectedResult = "5 порций")]
        [TestCase(11, ExpectedResult = "11 порций")]
        [TestCase(12, ExpectedResult = "12 порций")]
        [TestCase(21, ExpectedResult = "21 порция")]
        [TestCase(22, ExpectedResult = "22 порции")]
        [TestCase(25, ExpectedResult = "25 порций")]
        [TestCase(101, ExpectedResult = "101 порция")]
        [TestCase(102, ExpectedResult = "102 порции")]
        [TestCase(105, ExpectedResult = "105 порций")]
        public string FormatPortionsNumber_ValidNumber_ReturnsFormattedString(int number)
        {
            return number.FormatPortionsNumber();
        }
    }
}