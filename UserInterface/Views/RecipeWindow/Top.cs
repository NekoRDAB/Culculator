using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Culculator.Application.Extensions;

namespace UserInterface.Views;

class Top : Panel
{
    public Top(Dish dish)
    {
        var dishShortInfo = new StackPanel()
        {
            Margin = new Thickness(20, 10),
            Spacing = 10
        };
        dishShortInfo.Children.Add(FormDishNamePanel(dish));
        var shortInfo = new RoundBorder(1)
        {
            Child = new TextBlock
            {
                Margin = new Thickness(10),
                Text = $"{Math.Round(dish.Price, 2)} руб."
                       + "\n" + dish.NumberOfPortions.FormatPortionsNumber()
                       + "\n" + $"{Math.Round(dish.PricePerPortion, 2)} руб. за порцию",
                FontSize = 30,
                FontWeight = FontWeight.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Left
            }
        };
        dishShortInfo.Children.Add(shortInfo);
        Children.Add(dishShortInfo);
        // Children.Add(new DishImage(dish));
    }
    
    private Panel FormDishNamePanel(Dish dish)
    {
        var panel = new Panel();
        var border = new RoundBorder(1);
        var dishNameTextBlock = new TextBlock()
        {
            Margin = new Thickness(10),
            Text = dish.Name,
            FontSize = 30,
            FontWeight = FontWeight.SemiBold,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        border.Child = dishNameTextBlock;
        panel.Children.Add(border);

        return panel;
    }
}