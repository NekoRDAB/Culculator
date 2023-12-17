using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Castle.Core.Internal;
using Culculator.Application.Extensions;
using ReactiveUI;

namespace UserInterface.Views.IngredientAddition
{
    public class IngredientInput : Panel
    {
        private MainWindow _parent;

        public IngredientInput(MainWindow parent, IRepository repository)
        {
            VerticalAlignment = VerticalAlignment.Center;
            _parent = parent;
            Height = 100;
            Width = 800;

            var mainStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            var horizontalStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            var completionTextBlock = new TextBlock();
            var name = new TextBox
            {
                Watermark = "Название",
                Width = 300,
                Height = 50,
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5)
            };
            horizontalStackPanel.Children.Add(name);

            var price = new TextBox
            {
                Watermark = "Цена",
                Width = 200,
                Height = 50,
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5)
            };

            price.KeyDown += (sender, args) =>
            {
                var inputText = args.Key.ToString();
                var isNumeric = inputText.IsNumeric(price.Text, true, true);
                var caretIndex = price.CaretIndex;
                if (!string.IsNullOrEmpty(price.Text) && price.Text.Contains("0") && !price.Text.Contains(".") &&
                    inputText == "D0")
                    isNumeric = false;
                if (!string.IsNullOrEmpty(price.Text) && price.Text.Contains("0.") && caretIndex <= 1)
                    isNumeric = false;
                args.Handled = !isNumeric;
            };

            horizontalStackPanel.Children.Add(price);

            var measurementUnit = new ComboBox
            {
                Name = "MeasurementUnit",
                Width = 100,
                Height = 50,
                FontSize = 20,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5)
            };
            measurementUnit.Items.Add("Кг");
            measurementUnit.Items.Add("Л");
            measurementUnit.Items.Add("Шт");
            measurementUnit.SelectedItem = "Шт";

            horizontalStackPanel.Children.Add(measurementUnit);

            mainStackPanel.Children.Add(horizontalStackPanel);

            mainStackPanel.Children.Add(new Button
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Width = 200,
                Height = 40,
                Content = "Добавить",
                Margin = new Thickness(10),
                Command = ReactiveCommand.Create(() =>
                {
                    var measurement = measurementUnit.SelectedItem.ToString();
                    repository.AddIngredientToPersonalDB(new IngredientEntry
                    {
                        Name = name.Text ?? " ",
                        Price = price.Text.GetPriceByMeasurementUnit(measurement),
                        MeasurementUnit = measurement.ReformatMeasurementUnit()
                    });
                    completionTextBlock.Text = $"Индгредиент \"{name.Text}\" добавлен";
                    name.Text = "";
                    price.Text = "";
                    measurementUnit.SelectedItem = "";
                })
            });
            mainStackPanel.Children.Add(completionTextBlock);

            Children.Add(mainStackPanel);
        }
    }
}
