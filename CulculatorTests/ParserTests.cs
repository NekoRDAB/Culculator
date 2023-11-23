using NUnit.Framework;
using Moq;
using Culculator.Domain;
using Culculator.Infrastructure;
using Culculator.Application;
using System.Collections.Generic;

[TestFixture]
public class ParserTests
{
    [Test]
    public void ParseIngredient_ShouldParseIngredientEntryString()
    {
        var ingredientEntryString = "Tomato 5";
        var repository = new Mock<IRepository>();
        repository.Setup(r => r.GetIngredientFromDB("Tomato"))
            .Returns(new IngredientEntry { Name = "Tomato", Price = 2.50, MeasurementUnit = "piece" });
        
        var ingredient = Parser.ParseIngredient(1, ingredientEntryString, repository.Object);
        
        Assert.AreEqual("Tomato", ingredient.Name);
        Assert.AreEqual(5, ingredient.Amount);
        Assert.AreEqual("piece", ingredient.Measurement);
        Assert.AreEqual(2.50 * 5, ingredient.Price);
    }

    [Test]
    public void ParseIngredients_ShouldParseDishEntryIngredients()
    {
        var dishEntry = new DishEntry { Ingredients = "Tomato 5; Onion 2; Garlic 3" };
        var repository = new Mock<IRepository>();
        repository.Setup(r => r.GetIngredientFromDB(It.IsAny<string>()))
            .Returns<string>(name =>
                new IngredientEntry { Name = name, Price = 1.0, MeasurementUnit = "piece" });
        
        var ingredients = Parser.ParseIngredients(dishEntry, repository.Object);
        
        Assert.AreEqual(3, ingredients.Count);
        
        Assert.AreEqual("Tomato", ingredients[0].Name);
        Assert.AreEqual(5, ingredients[0].Amount);
        Assert.AreEqual("piece", ingredients[0].Measurement);
        Assert.AreEqual(1.0 * 5, ingredients[0].Price);
        
        Assert.AreEqual("Onion", ingredients[1].Name);
        Assert.AreEqual(2, ingredients[1].Amount);
        Assert.AreEqual("piece", ingredients[1].Measurement);
        Assert.AreEqual(1.0 * 2, ingredients[1].Price);
        
        Assert.AreEqual("Garlic", ingredients[2].Name);
        Assert.AreEqual(3, ingredients[2].Amount);
        Assert.AreEqual("piece", ingredients[2].Measurement);
        Assert.AreEqual(1.0 * 3, ingredients[2].Price);
    }
}