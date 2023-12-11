using System.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using ReactiveUI;
using SkiaSharp;

namespace UserInterface.Views.IngredientAddition;

public class IngredientInput : Panel
{
    private MainWindow _parent;
    public IngredientInput(MainWindow parent, IRepository repository)
    {
        VerticalAlignment = VerticalAlignment.Center;
        _parent = parent;
        Height = 100;
        Width = 500;
        var name = new TextBox
        {
            Name = "IngredientName",
            Watermark = "Введите название ингредиента",
            Width = 200,
            Height = 40,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };
        Children.Add(name);
        var price = new TextBox
        {
            Name = "Price",
            Watermark = "Введите цену за единицу ингредиента",
            Width = 100,
            Height = 40,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        };
        Children.Add(price);
        var measurementUnit = new TextBox
        {
            Name = "MeasurementUnit",
            Watermark = "Введите единицу измерения",
            HorizontalAlignment = HorizontalAlignment.Right,
            Width = 100,
            Height = 40,
            VerticalAlignment = VerticalAlignment.Top
        };
        Children.Add(measurementUnit);
        Children.Add(new Button
        {
            VerticalAlignment = VerticalAlignment.Bottom,
            Width = 200,
            Height = 40,
            HorizontalAlignment = HorizontalAlignment.Center,
            Content = "Добавить",
            Command = ReactiveCommand.Create(
                () =>
                {
                    repository.AddIngredientToPersonalDB(new IngredientEntry
                    {
                        Name = name.Text ?? " ",
                        Price = int.Parse(price.Text ?? "0"),
                        MeasurementUnit = measurementUnit.Text ?? "шт."
                    });
                })
        });
    }
}