using Culculator.Domain;
using Culculator.Infrastructure;
using System;

namespace Culculator
{
    public class Program
    {
        public static void Main()
        {
            var dbContext = new AddedRecipeContext();  // Assuming AddedRecipeContext is your DbContext class

            // Create a new DishEntry instance with some sample data
            var newDishEntry = new DishEntry
            {
                Name = "Sample Dish",
                Category = "Sample Category",
                Ingredients = "Ingredient1, Ingredient2",
                PortionsAmount = 4,
                RecipeInfo = "Sample Recipe Info"
            };

            // Add the new DishEntry to the DishEntries DbSet
            dbContext.AddedRecipesDataBase.Add(newDishEntry);

            // Save changes to the database
            dbContext.SaveChanges();

            // Optional: Display the newly added entry
            var addedEntry = dbContext.AddedRecipesDataBase.Find(newDishEntry.Id);
            if (addedEntry != null)
            {
                Console.WriteLine($"Added DishEntry: {addedEntry.Name}");
            }
        }
    }
}