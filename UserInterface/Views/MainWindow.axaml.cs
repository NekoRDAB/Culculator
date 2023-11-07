using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

public partial class MainWindow : Window
{
    public static MainWindow _this;

    public MainWindow()
    {
        _this = this;
        InitializeComponent();
        Content = new MainMenu();
    }
}

public class MainMenu : StackPanel
{
    public MainMenu()
    {
        Margin = new(20);
        Spacing = 7;
        Children.Add(new Title());
        Children.Add(new CategoriesPanel());
    }
}

class Title : StackPanel
{
    public Title()
    {
        Children.Add(new TextBlock
        {
            Text = "Culculator", FontSize = 50,
            TextAlignment = TextAlignment.Center
        });
        Spacing = -7;
        Children.Add(new TextBlock
        {
            Text = "Culinary Culculator", FontSize = 15, Foreground = Brushes.LightGray,
            TextAlignment = TextAlignment.Center
        });
        Margin = new Thickness(10);
    }
}

public class CategoriesPanel : StackPanel
{
    public CategoriesPanel()
    {
        Margin = new(20);
        Spacing = 10;
        foreach (var category in Categories.All)
            Children.Add(new Button
            {
                Width = 270,
                Height = 60,
                CornerRadius = new(10),
                Content = category.Name,
                FontSize = 16,
                Command = ReactiveCommand.Create(
                    () => { MainWindow._this.Content = new DishesMenu(category); })
            });
    }
}

class CategoryButton : Button // кнопки не отображаются по какой-то причине
{
    public CategoryButton(string categoryName)
    {
        Width = 270;
        Height = 70;
        CornerRadius = new(10);
        Content = categoryName;
    }
}

class DishesMenu : Panel
{
    public DishesMenu(Category category)
    {
        Children.Add(new DishesList(category));
        Children.Add(new Button
        {
            Width = 200,
            Height = 40,
            Margin = new(20),
            Content = "В главное меню",
            FontSize = 16,
            Background = null,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            CornerRadius = new(10),
            Command = ReactiveCommand.Create(
                () => { MainWindow._this.Content = new MainMenu(); })
        });
    }
}

class DishesList : StackPanel
{
    public DishesList(Category category)
    {
        Margin = new(20);
        Spacing = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        foreach (var dish in category.Dishes)
            Children.Add(new DishBox(dish));
    }
}

class DishBox : Panel
{
    public DishBox(Dish dish)
    {
        Width = 400;
        Height = 90;
        Children.Add(new DishBackground(Width, Height));
        Children.Add(new DishName(dish.Name));
        Children.Add(new DishPrice(dish.Price));
        Children.Add(new DishPortionsCount(dish.NumberOfPortions));
        Children.Add(new DishPricePerPortion(dish.PricePerPortion));
        Children.Add(new Button
        {
            Width = 100,
            Height = 20,
            Margin = new(10),
            FontSize = 10,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            Padding = new(0),
            Content = "Ингредиенты"
        });
    }

    private class DishBackground : Rectangle
    {
        public DishBackground(double width, double height)
        {
            Width = width;
            Height = height;
            Fill = Brushes.LightGray;
            RadiusX = 10;
            RadiusY = 10;
        }
    }

    private class DishName : TextBlock
    {
        public DishName(string name)
        {
            Margin = new(10);
            Text = name;
            FontSize = 16;
        }
    }

    private class DishPrice : TextBlock
    {
        public DishPrice(double price)
        {
            Margin = new(10);
            HorizontalAlignment = HorizontalAlignment.Right;
            Text = $"{(int)price} руб.";
        }
    }

    private class DishPortionsCount : TextBlock
    {
        public DishPortionsCount(int count)
        {
            Margin = new(10);
            HorizontalAlignment = HorizontalAlignment.Right;
            VerticalAlignment = VerticalAlignment.Center;
            Foreground = Brushes.Gray;
            Text = $"{count} порций";
        }
    }

    private class DishPricePerPortion : TextBlock
    {
        public DishPricePerPortion(double price)
        {
            Margin = new(10);
            HorizontalAlignment = HorizontalAlignment.Right;
            VerticalAlignment = VerticalAlignment.Bottom;
            Foreground = Brushes.Gray;

            Text = $"{(int)price} руб. за порцию";
        }
    }
}

class ReturnToMainMenuButton : Button
{
    public ReturnToMainMenuButton()
    {
        Width = 200;
        Height = 40;
        Margin = new(20);
        Content = "В главное меню";
        FontSize = 16;
        Background = null;
        VerticalAlignment = VerticalAlignment.Bottom;
        HorizontalAlignment = HorizontalAlignment.Left;
        Command = ReactiveCommand.Create(
            () => { MainWindow._this.Content = new MainMenu(); });
    }
}