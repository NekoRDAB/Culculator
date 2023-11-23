namespace CulculatorTests;

[TestFixture]
public class IngridientTest
{
    [Test]
    public void TestToString()
    {
        var ingredient = new Ingredient(1, "Ingr1", 2, "Kg", 21);
        Assert.AreEqual("1.Ingr1, 2 Kg - 42руб.", ingredient.ToString());
    }

    [Test]
    public void TestCreationFromBD()
    {
        var ingredient = new Ingredient(2, "Фарш 3", new Repository());
        Assert.AreEqual(new Ingredient(2, "Фарш", 3, "Гр.", 0.3), ingredient);
    }
}