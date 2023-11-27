using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace UserInterface.Views;

class DishPrice : TextBlock
{
    public DishPrice(double price)
    {
        Margin = new Thickness(10);
        HorizontalAlignment = HorizontalAlignment.Right;
        Text = $"{Math.Round(price, 2)} руб.";
        FontSize = 22;
    }
}