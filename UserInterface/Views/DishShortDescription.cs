using Avalonia.Controls;

namespace UserInterface.Views;

class DishShortDescription : Panel
{
    public DishShortDescription(Dish dish)
    {
        var priceTextBlock = new DishPrice(dish.Price);
        Children.Add(priceTextBlock);
        Children.Add(new InputPortionsCount(dish, priceTextBlock));
        Children.Add(new DishPricePerPortion(dish.PricePerPortion));
    }
}