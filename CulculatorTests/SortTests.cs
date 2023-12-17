namespace CulculatorTests;

[TestFixture]
[TestOf(typeof(PortionPriceSort))]
public class SortTests
{
    private Category _category;
    [SetUp]
    public void Setup()
    {
        _category = new Category("Main Courses", new List<Dish>
        {
            new Dish(new List<Ingredient>()
            {
                new(1, "Flour", 500, "Гр.", 2.50)
            }, 4, "", "Pasta", ""),
            new Dish(new List<Ingredient>()
                {
                    new(2, "Sugar", 250, "Гр.", 1.00)
                }, 8, "", "Pizza", ""),
            new Dish(new List<Ingredient>()
            {
                new(3, "Eggs", 4, "Шт.", 0.50)
            }, 5, "", "Burger", ""),
        });
    }
    [Test]
    public void SortAscending_ShouldReturnCategoryWithDishesSortedByAscendingPricePerPortion()
    {
        var sortMethod = new PortionPriceSort();
        
        var result = sortMethod.SortAscending(_category);
        
        Assert.AreEqual("Main Courses", result.Name);
        Assert.AreEqual(0.40, result.Dishes[0].PricePerPortion);
        Assert.AreEqual(31.25, result.Dishes[1].PricePerPortion);
        Assert.AreEqual(312.50, result.Dishes[2].PricePerPortion);
    }

    [Test]
    public void SortDescending_ShouldReturnCategoryWithDishesSortedByDescendingPricePerPortion()
    {
        var sortMethod = new PriceSort();
        
        var result = sortMethod.SortDescending(_category);
        
        Assert.AreEqual(1250, result.Dishes[0].Price);
        Assert.AreEqual(250, result.Dishes[1].Price);
        Assert.AreEqual(2, result.Dishes[2].Price);
    }
}