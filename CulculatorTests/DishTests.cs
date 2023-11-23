namespace CulculatorTests;

[TestFixture]
public class DishTests
{
     private DishEntry _dishEntry;
     private Dish _dish;
        
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
            _dish = new Dish(Parser.ParseIngredients(_dishEntry, new Repository()), _dishEntry.PortionsAmount,
                _dishEntry.RecipeInfo, _dishEntry.Name, _dishEntry.Category);
        }

        [Test]
        public void DishConstructor_ShouldSetPropertiesCorrectly()
        {
            
            Assert.AreEqual(_dishEntry.Name, _dish.Name);
            Assert.AreEqual(_dishEntry.Category, _dish.Category);
            Assert.AreEqual(_dishEntry.RecipeInfo, _dish.Recipe);
            Assert.AreEqual(_dishEntry.PortionsAmount, _dish.NumberOfPortions);
            Assert.AreEqual(45, _dish.Price);
            Assert.AreEqual(15, _dish.PricePerPortion);
            Assert.AreEqual(2, _dish.Ingredients.Count);
        }

        [Test]
        public void ToString_ShouldReturnCorrectStringRepresentation()
        {

            var result = _dish.ToString();

            var expectedString = $"Овсяная каша на молоке\n\n" +
                                 $"Овсянка, 150 Гр. - 15руб.\n" +
                                 $"Молоко, 500 Мл. - 30руб.\n" +
                                 $"Варите овсянку с молоком на молоке 20 минут постоянно помешивая.\n\n" +
                                 $"45руб.\n" +
                                 $"3\n" +
                                 $"15руб/порция";

            Assert.AreEqual(expectedString, result);
        }
        
        [Test]
        public void FormatRecipe_ShouldReturnFormattedRecipeString_WhenRecipeHasMultipleSteps()
        {
            var recipe = "Prepare ingredients. Cook the dish. Serve hot.";
            var dish = new Dish(Array.Empty<Ingredient>(), 0, recipe, "", "");
            
            var formattedRecipe = dish.FormatRecipe();
            
            var expectedFormattedRecipe = "1. Prepare ingredients\r\n2. Cook the dish\r\n3. Serve hot\r\n";
            Assert.AreEqual(expectedFormattedRecipe, formattedRecipe);
        }

        [Test]
        public void FormatRecipe_ShouldReturnFormattedRecipeString_WhenRecipeHasSingleStep()
        {
            var recipe = "Mix all ingredients.";
            var dish = new Dish(Array.Empty<Ingredient>(), 0, recipe, "", "");

            // Act
            var formattedRecipe = dish.FormatRecipe();

            // Assert
            var expectedFormattedRecipe = "1. Mix all ingredients\r\n";
            Assert.AreEqual(expectedFormattedRecipe, formattedRecipe);
        }

        [Test]
        public void FormatRecipe_ShouldReturnEmptyString_WhenRecipeIsEmpty()
        {
            var recipe = "";
            var dish = new Dish(Array.Empty<Ingredient>(), 0, recipe, "", "");
            
            var formattedRecipe = dish.FormatRecipe();
            
            Assert.AreEqual("", formattedRecipe);
        }
}