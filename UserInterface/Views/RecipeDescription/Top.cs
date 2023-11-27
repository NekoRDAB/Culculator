using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class Top : Panel
{
    public Top(Dish dish)
    {
        Children.Add(new DishShortInfo(dish));
        // Children.Add(new DishImage(dish));
    }

    class DishShortInfo : StackPanel
    {
        public DishShortInfo(Dish dish)
        {
            Margin = new Thickness(20, 10);
            Spacing = 10;
            Children.Add(new DishName(dish.Name));
            Children.Add(new RoundBorder(1)
            {
                Child = new TextBlock
                {
                    Margin = new Thickness(10),
                    Text = new DishPrice(dish.Price).Text
                           + "\n" + new DishPortionsCount(dish.NumberOfPortions).Text
                           + "\n" + new DishPricePerPortion(dish.PricePerPortion).Text,
                    FontSize = 30,
                    FontWeight = FontWeight.SemiBold,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Left
                }
            });
        }

        private class DishName : Panel
        {
            public DishName(string name)
            {
                Children.Add(new RoundBorder(1)
                {
                    Child = new TextBlock
                    {
                        Margin = new Thickness(10),
                        Text = name,
                        FontSize = 30,
                        FontWeight = FontWeight.SemiBold,
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Left
                    }
                });
            }
        }
    }
}