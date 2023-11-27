using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace UserInterface.Views;

class DishPricePerPortion : TextBlock
{
    public DishPricePerPortion(double price)
    {
        Margin = new Thickness(10);
        HorizontalAlignment = HorizontalAlignment.Right;
        VerticalAlignment = VerticalAlignment.Bottom;
        Text = $"{Math.Round(price, 2)} руб. за порцию";
        FontSize = 22;
    }
}