using System;
using Avalonia;
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
        var dishPriceTextBlock = FormDishPrice(dish);
        Children.Add(dishPriceTextBlock);

        var inputPortionsCountButton = new BaseTargetButton(null, null, CreateTextBox(dish, dishPriceTextBlock), Brushes.Transparent, Brushes.Transparent,
            null, VerticalAlignment.Center, HorizontalAlignment.Right, null);
        Children.Add(inputPortionsCountButton);
        Children.Add(FormDishPricePerPortion(dish));
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
            var caretIndex = textBox.CaretIndex;
            if (inputText == "D0" && caretIndex == 0)
                isNumeric = false;
            args.Handled = !isNumeric;
        };

        var contentControl = new ContentControl { Content = textBox };
        return contentControl;
    }

    private TextBlock FormDishPrice(Dish dish)
    {
        var dishPriceTextBlock = new TextBlock()
        {
            Margin = new Thickness(10),
            HorizontalAlignment = HorizontalAlignment.Right,
            Text = $"{Math.Round(dish.Price, 2)} руб.",
            FontSize = 22,
        };
        return dishPriceTextBlock;
    }

    private TextBlock FormDishPricePerPortion(Dish dish)
    {
        var dishPricePerPortionTextBlock = new TextBlock()
        {
            Margin = new Thickness(10),
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Text = $"{Math.Round(dish.PricePerPortion, 2)} руб. за порцию",
            FontSize = 22
        };
        return dishPricePerPortionTextBlock;
    }
}