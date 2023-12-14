using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Castle.Core.Internal;
using Culculator.Application.Extensions;

namespace UserInterface.Views;

class DishShortDescription : Panel
{
    private readonly Dish _dish;
    public DishShortDescription(Dish dish)
    {
        _dish = dish;
        var priceTextBlock = new DishPrice(dish.Price);
        Children.Add(priceTextBlock);

        var inputPortionsCountButton = new BaseTargetButton(null, null, CreateTextBox(dish, priceTextBlock), Brushes.Transparent, Brushes.Transparent,
            null, VerticalAlignment.Center, HorizontalAlignment.Right, null);
        Children.Add(inputPortionsCountButton);
        Children.Add(new DishPricePerPortion(dish.PricePerPortion));
    }
    
    private static ContentControl CreateTextBox(Dish dish, TextBlock priceTextBlock)
    {
        var textBox = new TextBox
        {
            Text = dish.NumberOfPortions.ToString(),
            FontSize = 22,
            MaxLength = 3,
        };

        textBox.KeyUp += (sender, args) =>
        {
            if (!textBox.Text.IsNullOrEmpty())
            {
                dish.RecalculateTotalPrice(int.Parse(textBox.Text));
                priceTextBlock.Text = $"{Math.Round(dish.Price, 2)} руб.";
            }
        };

        textBox.KeyDown += (sender, args) =>
        {
            var inputText = args.Key.ToString();
            var isNumeric = inputText.IsNumeric(textBox.Text);
            args.Handled = !isNumeric;
        };

        var contentControl = new ContentControl { Content = textBox };
        return contentControl;
    }
}