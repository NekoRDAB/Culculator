using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Castle.Core.Internal;
using Culculator.Application.Extentions;

namespace UserInterface.Views;

class InputPortionsCount : Border
{
    private readonly Dish _dish;
    private readonly TextBlock _priceTextBlock;

    public InputPortionsCount(Dish dish, TextBlock priceTextBlock)
    {
        _dish = dish;
        _priceTextBlock = priceTextBlock;
        var textBox = new TextBox
        {
            Margin = new Thickness(5),
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = Brushes.Gray,
            Text = dish.NumberOfPortions.ToString(),
            FontSize = 22,
            MaxLength = 3
        };

        textBox.KeyUp += (sender, args) =>
        {
            if (!textBox.Text.IsNullOrEmpty())
            {
                _dish.RecalculateTotalPrice(int.Parse(textBox.Text));
                _priceTextBlock.Text = $"{Math.Round(_dish.Price, 2)} руб.";
            }
        };
        
        textBox.KeyDown += (sender, args) =>
        {
            var inputText = args.Key.ToString();
            var isNumeric = inputText.IsNumeric(textBox.Text);
            args.Handled = !isNumeric;
        };

        Child = textBox;
    }

    private void RecalculateTotalPrice()
    {
        if (double.TryParse(((TextBox)Child).Text, out double newPortionCount))
        {
            var coef = newPortionCount / _dish.NumberOfPortions;
            _dish.Price = _dish.PricePerPortion * newPortionCount;
            _dish.NumberOfPortions = (int)newPortionCount;
            _priceTextBlock.Text = $"{Math.Round(_dish.Price, 2)} руб.";
            foreach (var ingredient in _dish.Ingredients)
                ingredient.Amount *= coef;
        }
    }
}