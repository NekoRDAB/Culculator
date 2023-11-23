namespace CulculatorTests;

[TestFixture]
public class DishTests
{
     private DishEntry _dishEntry;
        
        [SetUp]
        public void SetUp()
        {
            _dishEntry = new DishEntry
            {
                Name = "Овсяная каша на молоке",
                Category = "Завтраки",
                RecipeInfo = "Варите овсянку с молоком на молоке 20 минут постоянно помешивая.",
                Ingredients = "Овсянка 150; Молоко 500",
                PortionsAmount = 3
            };
        }

        [Test]
        public void DishConstructor_ShouldSetPropertiesCorrectly()
        {
            var dish = new Dish(_dishEntry, new Repository());
            
            Assert.AreEqual(_dishEntry.Name, dish.Name);
            Assert.AreEqual(_dishEntry.Category, dish.Category);
            Assert.AreEqual(_dishEntry.RecipeInfo, dish.Recipe);
            Assert.AreEqual(_dishEntry.PortionsAmount, dish.NumberOfPortions);
            Assert.AreEqual(45, dish.Price);
            Assert.AreEqual(15, dish.PricePerPortion);
            Assert.AreEqual(2, dish.Ingredients.Count);
        }

        [Test]
        public void ToString_ShouldReturnCorrectStringRepresentation()
        {
            var dish = new Dish(_dishEntry, new Repository());
            
            var result = dish.ToString();
            
            var expectedString = $"Овсяная каша на молоке\n\n" +
                $"1.Овсянка, 150 Гр. - 15руб.\n" +
                $"2.Молоко, 500 Мл. - 30руб.\n" +
                $"Варите овсянку с молоком на молоке 20 минут постоянно помешивая.\n\n" +
                $"45руб.\n" +
                $"3\n" +
                $"15руб/порция";
            
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void CollectIngredients_ShouldReturnCorrectListOfIngredients()
        {
            var dish = new Dish(_dishEntry, new Repository());
            var expectedIngredients = new[]
            {
                new Ingredient(1, "Овсянка", 150, "Гр.", 0.1),
                new Ingredient(2, "Молоко", 500, "Мл.", 0.06),
            };
            
            var ingredients = dish.Ingredients.ToArray();
            
            Assert.AreEqual(expectedIngredients.Length, ingredients.Length);
            for (int i = 0; i < expectedIngredients.Length; i++)
            {
                Assert.AreEqual(expectedIngredients[i].Name, ingredients[i].Name);
                Assert.AreEqual(expectedIngredients[i].Amount, ingredients[i].Amount);
                Assert.AreEqual(expectedIngredients[i].Measurement, ingredients[i].Measurement);
                Assert.AreEqual(expectedIngredients[i].Price, ingredients[i].Price);
            }
        }
}