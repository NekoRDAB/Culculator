namespace CulculatorTests;

[TestFixture]
public class IngridientShould
{
    [SetUp]
    public static void SetUp()
    {
        var ing = new Ingredient(1, "2", 3, MeasurementUnit.Kilogram);
    }
}