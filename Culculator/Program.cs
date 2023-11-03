using System.ComponentModel.DataAnnotations.Schema;
using Culculator.DataBase;
using Culculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace Culculator
{
    [Table("IngredientsDB")]
    internal class Program
    {
        static void Main(string[] args)
        {
            var parserInstance = new Parser();
            var egg = parserInstance.GetIngredientFromDB("Яйцо");
            Console.WriteLine(egg.GetType());
            Console.WriteLine($"{egg.id}, {egg.Name}, {egg.Price}");
            var nothing = parserInstance.GetIngredientFromDB("Абракадабра");
            // var egg = new Ingredient("Яйцо", 7);
            // var sugar = new Ingredient("Сахар", 0.06);
            // using (var db = new IngredientsContext())
            // {
            //     db.IngredientsDataBase.Add(egg);
            //     db.IngredientsDataBase.Add(sugar);
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