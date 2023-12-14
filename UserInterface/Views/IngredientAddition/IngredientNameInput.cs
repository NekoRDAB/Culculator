using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
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
                    repository.AddIngredientToPersonalDB(new IngredientEntry
                    {
                        Name = name.Text ?? " ",
                        Price = GetPrice(price.Text, measurementUnit.SelectedItem.ToString()),
                        MeasurementUnit = GetMeasurementUnit(measurementUnit.SelectedItem.ToString())
                    });
                })
            });

            Children.Add(mainStackPanel);
        }

        private double GetPrice(string price, string measurementUnit)
        {
            if (price == null)
                return 0;
            switch (measurementUnit)
            {
                case "Шт":
                    return int.Parse(price);
                case "Кг":
                case "Л":
                    return double.Parse(price) / 1000;
                default:
                    return 0;
            }
        }

        private string GetMeasurementUnit(string measurementUnit)
        {
            return measurementUnit switch
            {
                "Кг" => "Гр.",
                "Л" => "Мл.",
                _ => "Шт."
            };
        }
    }
}
