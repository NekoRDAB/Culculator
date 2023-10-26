namespace Culculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dish = new Dish("aboba",
                "aaaaaaaaa,adsadasd",
                3,
                DishCategory.Breakfast,
                new[] { new Ingredient(1, "amogus", 2, MeasurementUnit.Tbsp) });
            Console.WriteLine(dish);
        }
    }
}