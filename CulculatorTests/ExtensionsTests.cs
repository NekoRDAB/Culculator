using Culculator.Application.Extensions;

namespace CulculatorTests;

[TestFixture]
public class ExtensionsTests
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

    [Test]
    public void RecalculateTotalPrice_ShouldUpdatePriceAndNumberOfPortions()
    {
        var dish = new Dish(new List<Ingredient>
            {
                new(1, "Lettuce", 100, "Гр.", 0.50),
                new(2, "Tomatoes", 200, "Гр.", 0.75),
                new(3, "Cucumbers", 50, "Гр.", 0.60)
            },
            1,
            "aaaaa",
            "Salad",
            "Salads"
        );

        dish.RecalculateTotalPrice(4);

        Assert.AreEqual(4, dish.NumberOfPortions);
        Assert.AreEqual(920, dish.Price);
        Assert.AreEqual(400, dish.Ingredients[0].Amount);
        Assert.AreEqual(800, dish.Ingredients[1].Amount);
        Assert.AreEqual(200, dish.Ingredients[2].Amount);
    }

    [Test]
    public void RecalculateTotalPrice_ShouldNotChangePriceIfNumberOfPortionsRemainsTheSame()
    {
        var dish = new Dish(new List<Ingredient>
            {
                new(1, "Lettuce", 100, "Гр.", 0.50),
                new(2, "Tomatoes", 200, "Гр.", 0.75),
                new(3, "Cucumbers", 150, "Гр.", 0.60)
            },
            2,
            "aaaaa",
            "Salad",
            "Salads"
        );

        dish.RecalculateTotalPrice(4);

        Assert.AreEqual(4, dish.NumberOfPortions);
        Assert.AreEqual(580, dish.Price);
    }

    [Test]
    public void IsNumeric_ShouldReturnFalseForDotKeyWhenCurrentTextIsNotEmptyAndContainsDot()
    {
        var key = "OemPeriod";
        var currentText = "3.14";

        var result = key.IsNumeric(currentText, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsNumeric_ShouldReturnFalseForZeroKeyWhenZeroFirstIsNotAllowedAndCurrentTextIsEmpty()
    {
        var key = "D0";
        var currentText = "";

        var result = key.IsNumeric(currentText);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsNumeric_ShouldReturnTrueForZeroKeyWhenZeroFirstIsAllowed()
    {
        var key = "D0";
        var currentText = "";

        var result = key.IsNumeric(currentText, false, true);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsNumeric_ShouldReturnFalseForDotKeyWhenCurrentTextIsEmptyAndDotIsNotAllowed()
    {
        var key = "OemPeriod";
        var currentText = "";

        var result = key.IsNumeric(currentText);

        Assert.IsFalse(result);
    }

    [Test]
    public void ReformatMeasurementUnit_ShouldReturnCorrectFormattedUnit()
    {
        var unit1 = "Кг";
        var unit2 = "Л";
        var unit3 = "шт";

        var result1 = unit1.ReformatMeasurementUnit();
        var result2 = unit2.ReformatMeasurementUnit();
        var result3 = unit3.ReformatMeasurementUnit();

        Assert.AreEqual("Гр.", result1);
        Assert.AreEqual("Мл.", result2);
        Assert.AreEqual("Шт.", result3);
    }

    [Test]
    public void GetPriceByMeasurementUnit_ShouldReturnCorrectPriceForDifferentUnits()
    {
        var price1 = "2";
        var price2 = "1500";
        string price3 = null;
        var unit1 = "Шт";
        var unit2 = "Кг";
        var unit3 = "Л";

        var result1 = price1.GetPriceByMeasurementUnit(unit1);
        var result2 = price2.GetPriceByMeasurementUnit(unit2);
        var result3 = price3.GetPriceByMeasurementUnit(unit3);

        Assert.AreEqual(2, result1);
        Assert.AreEqual(1.5, result2);
        Assert.AreEqual(0, result3);
    }
}