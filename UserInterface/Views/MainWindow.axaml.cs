using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Culculator.Domain;
using ReactiveUI;

namespace UserInterface.Views;

public partial class MainWindow : Window
{
    static MainWindow _this;
    
    class BlackBorder : Border
    {
        public BlackBorder(double width, double height, int thickness)
        {
            BorderThickness = new Thickness(thickness);
            BorderBrush = Brushes.DarkSlateGray;
            Width = width;
            Height = height;
        }
    }

    public MainWindow()
    {
        Environment.CurrentDirectory = Environment.CurrentDirectory.Replace("bin\\Debug\\net6.0", "");
        _this = this;
        InitializeComponent();
        Width = 900;
        Height = 600;
        Content = new MainMenu();
    }

    class MainMenu : StackPanel
    {
        public MainMenu()
        {
            Margin = new(20);
            Spacing = 7;
            Children.Add(new Title());
            Children.Add(new CategoriesPanel());
        }

        class Title : StackPanel
        {
            public Title()
            {
                Children.Add(new TextBlock
                {
                    Text = "Culculator", FontSize = 80,
                    TextAlignment = TextAlignment.Center
                });
                Spacing = -7;
                Children.Add(new TextBlock
                {
                    Text = "Culinary Calculator", FontSize = 25, Foreground = Brushes.LightGray,
                    TextAlignment = TextAlignment.Center
                });
                Margin = new Thickness(10);
            }
        }
        
        class CategoriesPanel : StackPanel
        {
            public CategoriesPanel()
            {
                Margin = new(20);
                Spacing = 10;
                foreach (var category in Categories.All)
                    Children.Add(new CategoryButton(category));
            }

            class CategoryButton : Panel
            {
                public CategoryButton(Category category)
                {
                    
                    Children.Add(new Button
                    {
                        Width = 330,
                        Height = 75,
                        Content = category.Name,
                        FontSize = 25,
                        Command = ReactiveCommand.Create(
                            () => { _this.Content = new DishesMenu(category); })
                    });
                    Children.Add(new BlackBorder(330, 75, 1));
                }
            }
        }
    }

    class DishesMenu : Panel
    {
        public DishesMenu(Category category)
        {
            Children.Add(new ScrollViewer { Content = new DishesList(category) });
            Children.Add(new ReturnButton());
        }

        class DishesList : StackPanel
        {
            public DishesList(Category category)
            {
                Margin = new(20);
                Spacing = 10;
                HorizontalAlignment = HorizontalAlignment.Center;
                foreach (var dish in category.Dishes)
                    Children.Add(new DishBox(category, dish));
            }

            class DishBox : Panel
            {
                public DishBox(Category category, Dish dish)
                {
                    Width = 650;
                    Height = 120;
                    Children.Add(new DishDescriptionButton(category, dish, Width, Height));
                    Children.Add(new DishName(dish.Name));
                    Children.Add(new RightPart(dish));
                }

                class DishDescriptionButton : Panel
                {
                    public DishDescriptionButton(Category category, Dish dish, double width, double height)
                    {
                        Children.Add(new Button
                        {
                            Width = width,
                            Height = height,
                            Command = ReactiveCommand.Create(
                                () => { _this.Content = new DishDescription(category, dish); })
                        });
                        Children.Add(new BlackBorder(width, height, 1));
                    }
                }

                class DishName : TextBlock
                {
                    public DishName(string name)
                    {
                        Margin = new(10);
                        Text = name;
                        FontSize = 30;
                        MaxWidth = 420;  
                        FontWeight = FontWeight.SemiBold;
                        TextWrapping = TextWrapping.Wrap;
                        HorizontalAlignment = HorizontalAlignment.Left;
                    }
                }

                class RightPart : StackPanel
                {
                    public RightPart(Dish dish)
                    {
                        Orientation = Orientation.Horizontal;
                        HorizontalAlignment = HorizontalAlignment.Right;
                        Children.Add(new DishShortDescription(dish));
                        // Children.Add(new DishImage(dish));
                    }

                    class DishShortDescription : Panel
                    {
                        public DishShortDescription(Dish dish)
                        {
                            Children.Add(new DishPrice(dish.Price));
                            Children.Add(new DishPortionsCount(dish.NumberOfPortions));
                            Children.Add(new DishPricePerPortion(dish.PricePerPortion));
                        }

                        class DishPrice : TextBlock
                        {
                            public DishPrice(double price)
                            {
                                Margin = new(10);
                                HorizontalAlignment = HorizontalAlignment.Right;
                                Text = $"{(int)price} руб.";
                                FontSize = 22;
                            }
                        }

                        class DishPortionsCount : TextBlock
                        {
                            public DishPortionsCount(int count)
                            {
                                Margin = new(10);
                                HorizontalAlignment = HorizontalAlignment.Right;
                                VerticalAlignment = VerticalAlignment.Center;
                                Foreground = Brushes.Gray;
                                Text = $"{count} {DisplayPortionsNumber(count)}";
                                FontSize = 22;
                            }

                            private string DisplayPortionsNumber(int count)
                            {
                                switch (count)
                                {
                                    case 1:
                                        return "порция";
                                    case 2: case 3: case 4:
                                        return "порции";
                                    default:
                                        return "порций";
                                }
                            }
                        }

                        class DishPricePerPortion : TextBlock
                        {
                            public DishPricePerPortion(double price)
                            {
                                Margin = new(10);
                                HorizontalAlignment = HorizontalAlignment.Right;
                                VerticalAlignment = VerticalAlignment.Bottom;
                                Foreground = Brushes.Gray;
                                Text = $"{(int)price} руб. за порцию";
                                FontSize = 22;
                            }
                        }
                    }

                    class DishImage : Image
                    {
                        public DishImage(Dish dish)
                        {
                            Margin = new(10);
                            Width = 150;
                            Height = 100;
                            HorizontalAlignment = HorizontalAlignment.Right;
                            if (dish is DishWithImage dishImage)
                                try
                                {
                                    Source = new Avalonia.Media.Imaging.Bitmap(dishImage.PathToImage)
                                        .CreateScaledBitmap(new PixelSize((int)Width, (int)Height));
                                }
                                catch
                                {
                                    Console.WriteLine($"Изображение {dishImage.PathToImage} не найдено");
                                }
                        }
                    }
                }
            }
        }


        class ReturnButton : Panel
        {
            public ReturnButton()
            {
                Children.Add(new Button
                {
                    Width = 200,
                    Height = 40,
                    Margin = new(20),
                    Content = "В главное меню",
                    FontSize = 22,
                    Background = null,
                    Foreground = Brushes.DarkGray,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Command = ReactiveCommand.Create(
                        () => { _this.Content = new MainMenu(); })
                });
            }
        }
    }

    class DishDescription : Panel
    {
        public DishDescription(Category category, Dish dish)
        {
            Children.Add(new DishDescriptionPanels(dish));
            Children.Add(new ReturnButton(category));
        }

        class DishDescriptionPanels : StackPanel
        {
            public DishDescriptionPanels(Dish dish)
            {
                Children.Add(new Top(dish));
                Children.Add(new Bottom(dish));
            }

            class Top : Panel
            {
                public Top(Dish dish)
                {
                    Height = 270;
                    Children.Add(new DishShortInfo(dish));
                    // Children.Add(new DishImage(dish));
                }

                class DishShortInfo : StackPanel
                {
                    public DishShortInfo(Dish dish)
                    {
                        Margin = new(20, 10);
                        Children.Add(new DishName(dish.Name));
                        Children.Add(new DishPrice(dish.Price));
                        Children.Add(new DishPortionsCount(dish.NumberOfPortions));
                        Children.Add(new DishPricePerPortion(dish.PricePerPortion));
                    }

                    private class DishName : TextBlock
                    {
                        public DishName(string name)
                        {
                            Margin = new(7);
                            Text = name;
                            FontSize = 40;
                            MaxWidth = 1200;  
                            FontWeight = FontWeight.SemiBold;
                            TextWrapping = TextWrapping.Wrap;
                            HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }

                    private class DishPrice : TextBlock
                    {
                        public DishPrice(double price)
                        {
                            Margin = new(7, 3);
                            FontSize = 29;
                            Text = $"{(int)price} руб.";
                        }
                    }

                    private class DishPortionsCount : TextBlock
                    {
                        public DishPortionsCount(int count)
                        {
                            Margin = new(7, 3);
                            FontSize = 29;
                            Foreground = Brushes.Gray;
                            Text = $"{count} порций";
                        }
                    }

                    private class DishPricePerPortion : TextBlock
                    {
                        public DishPricePerPortion(double price)
                        {
                            Margin = new(7, 3);
                            FontSize = 29;
                            Foreground = Brushes.Gray;
                            Text = $"{(int)price} руб. за порцию";
                        }
                    }
                }

                class DishImage : Image
                {
                    public DishImage(Dish dish)
                    {
                        Margin = new(28);
                        Width = 300;
                        Height = 200;
                        HorizontalAlignment = HorizontalAlignment.Right;
                        VerticalAlignment = VerticalAlignment.Top;
                        if (dish is DishWithImage dishImage)
                            try
                            {
                                Source = new Avalonia.Media.Imaging.Bitmap(dishImage.PathToImage)
                                    .CreateScaledBitmap(new PixelSize((int)Width, (int)Height));
                            }
                            catch
                            {
                                Console.WriteLine($"Изображение {dishImage.PathToImage} не найдено");
                            }
                    }
                }
            }

            class Bottom : Panel
            {
                public Bottom(Dish dish)
                {
                    Children.Add(new Ingredients(dish));
                    Children.Add(new Recipe(dish));
                }

                class Ingredients : StackPanel
                {
                    public Ingredients(Dish dish)
                    {
                        Margin = new(20, 0);
                        HorizontalAlignment = HorizontalAlignment.Left;
                        Width = _this.Width / 2.2;
                        Children.Add(new Label());
                        Children.Add(new IngredientsList(dish));
                    }

                    class Label : TextBlock
                    {
                        public Label()
                        {
                            Text = "Ингредиенты";
                            Margin = new(10);
                            FontSize = 26;
                            FontWeight = FontWeight.SemiBold;
                            HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }

                    class IngredientsList : StackPanel
                    {
                        public IngredientsList(Dish dish)
                        {
                            foreach (var ingredient in dish.Ingredients)
                                Children.Add(new IngredientDescription(ingredient));
                        }

                        class IngredientDescription : Panel
                        {
                            public IngredientDescription(Ingredient ingredient)
                            {
                                Children.Add(new IngredientAndAmount(ingredient));
                            }

                            class IngredientAndAmount : TextBlock
                            {
                                public IngredientAndAmount(Ingredient ingredient)
                                {
                                    Margin = new(0);
                                    Text = ingredient.ToString();
                                    FontSize = 20;
                                    TextWrapping = TextWrapping.Wrap;
                                    HorizontalAlignment = HorizontalAlignment.Left;
                                }
                            }
                        }
                    }
                }

                class Recipe : StackPanel
                {
                    public Recipe(Dish dish)
                    {
                        Margin = new(20, 0);
                        HorizontalAlignment = HorizontalAlignment.Right;
                        Width = _this.Width / 2.2;
                        Children.Add(new Label());
                        Children.Add(new Steps(dish));
                    }

                    class Label : TextBlock
                    {
                        public Label()
                        {
                            Text = "Рецепт";
                            Margin = new(10);
                            FontSize = 26;
                            FontWeight = FontWeight.SemiBold;
                            HorizontalAlignment = HorizontalAlignment.Center;
                        }
                    }

                    class Steps : TextBlock
                    {
                        public Steps(Dish dish)
                        {
                            Text = string.Join(Environment.NewLine, dish.Recipe);
                            FontSize = 20;
                            TextWrapping = TextWrapping.Wrap;
                            HorizontalAlignment = HorizontalAlignment.Center;
                        }

                        class StepDescription : Panel
                        {
                            public StepDescription(string step)
                            {
                                Children.Add(new Step(step));
                            }

                            class Step : TextBlock
                            {
                                public Step(string step)
                                {
                                    Margin = new(0);
                                    Text = step;
                                    FontSize = 20;
                                    TextWrapping = TextWrapping.Wrap;
                                    HorizontalAlignment = HorizontalAlignment.Center;
                                }
                            }
                        }
                    }
                }
            }
        }

        class ReturnButton : Panel
        {
            public ReturnButton(Category category)
            {
                Children.Add(new Button
                {
                    Width = 200,
                    Height = 40,
                    Margin = new(20),
                    Content = "В меню блюд",
                    FontSize = 22,
                    Background = null,
                    Foreground = Brushes.DarkGray,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Command = ReactiveCommand.Create(
                        () => { _this.Content = new DishesMenu(category); })
                });
            }
        }
    }
}