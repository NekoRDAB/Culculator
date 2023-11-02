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
            Ingredient test = new Ingredient(10, "Abc", "Egg", 70);
            var test2 = new Ingredient(2, "Залупа", "Ебать", 10000);
            using (var db = new ApplicationContext())
            {
                db.IngredientsDB.Add(test);
                db.IngredientsDB.Add(test2);
                db.SaveChanges();
                // db.IngredientsDB.ExecuteDelete();
            }
            
            // var db = new ApplicationContext();
            // db.IngredientsDB.Add(test);
            // db.SaveChanges();
        }
    }
}