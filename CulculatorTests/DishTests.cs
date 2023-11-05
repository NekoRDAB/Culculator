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
                Name = "Test Dish",
                Category = "Test Category",
                RecipeInfo = "Test Recipe",
                Ingredients = "Ingredient 1; Ingredient 2; Ingredient 3",
                PortionsAmount = 2
            };
        }

        [Test]
        public void DishConstructor_ShouldSetPropertiesCorrectly()
        {
            var dish = new Dish(_dishEntry);
            
            Assert.AreEqual(_dishEntry.Name, dish.Name);
            Assert.AreEqual(_dishEntry.Category, dish.Category);
            Assert.AreEqual(_dishEntry.RecipeInfo, dish.Recipe);
            Assert.AreEqual(_dishEntry.PortionsAmount, dish.NumberOfPortions);
            Assert.AreEqual(600, dish.Price);
            Assert.AreEqual(300, dish.PricePerPortion);
            Assert.AreEqual(3, dish.Ingredients.Count);
        }

        [Test]
        public void ToString_ShouldReturnCorrectStringRepresentation()
        {
            var dish = new Dish(_dishEntry);
            
            var result = dish.ToString();
            
            var expectedString = $"Test Dish\n\n" +
                $"Ingredient 1: 100 г\n" +
                $"Ingredient 2: 200 г\n" +
                $"Ingredient 3: 300 г\n" +
                $"Test Recipe\n\n" +
                $"600руб.\n" +
                $"2\n" +
                $"300руб/порция";
            
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void CollectIngredients_ShouldReturnCorrectListOfIngredients()
        {
            var dish = new Dish(_dishEntry);
            var expectedIngredients = new[]
            {
                new Ingredient(1, "Ingredient 1", 100, "г", 10),
                new Ingredient(2, "Ingredient 2", 200, "г", 20),
                new Ingredient(3, "Ingredient 3", 300, "г", 30)
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