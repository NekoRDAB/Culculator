using Culculator.Domain;
using Culculator.Infrastructure;
namespace Culculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parserInstance = new Parser();

            var recipes = parserInstance.GetRecipesFromDbByCategory("Простые");
            foreach (var recipe in recipes)
            {
                var dish = new Dish(recipe);
                Console.WriteLine(dish);
            }
            
            // using (var recipeDB = new RecipesContext())
            // {
            //     var recipeName = "Яичница";
            //     var recipe = recipeDB.RecipesDataBase.FirstOrDefault(i => i.Name == recipeName);
            //     var ingredients = recipe.Ingredients;
            //     var a = parserInstance.CollectIngredients(ingredients);
            //     
            // }

            // var egg = parserInstance.GetIngredientFromDB("Яйцо");
            // Console.WriteLine(egg.GetType());
            // Console.WriteLine($"{egg.id}, {egg.Name}, {egg.Price}");
            // var nothing = parserInstance.GetIngredientFromDB("Абракадабра");

            // var sausage = new Ingredient("Сосиска", 12);
            // var tomato = new Ingredient("Помидор", 0.12);
            // using (var db = new IngredientsContext())
            // {
            //     db.IngredientsDataBase.Add(sausage);
            //     db.IngredientsDataBase.Add(tomato);
            //     db.SaveChanges();
            //     // db.IngredientsDB.ExecuteDelete();
            // }

            // var friedEggRecipe =
            //     new Recipe("Яичница", "Простые",
            //         "Яйцо 3; Сосиска 2; Помидор 200", 3, "Вкусная яишенка");
            // using (var recipeDB = new RecipesContext())
            // {
            //     recipeDB.RecipesDataBase.Add(friedEggRecipe);
            //     recipeDB.SaveChanges();
            // }
        }
    }
}

