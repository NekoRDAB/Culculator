using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

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
            RecalculateTotalPrice();
        };

        textBox.KeyDown += (sender, args) => { args.Handled = !IsNumeric(args.Key); };
        Child = textBox;
    }

    private bool IsNumeric(Key key)
    {
        if (key == Key.D0 || key == Key.NumPad0)
        {
            var textBox = (TextBox)Child;
            if (textBox.Text.Length == 0 || textBox.CaretIndex == 0)
                return false;
        }

        return (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9);
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