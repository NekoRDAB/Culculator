using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

public partial class MainWindow : Window
{
    private static bool ascendingOrder;
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
                            () => { _this.Content = new DishesMenu(category, ascendingOrder); })
                    });
                    Children.Add(new BlackBorder(330, 75, 1));
                }
            }
        }
    }

    class DishesMenu : Panel
    {
        public DishesMenu(Category category, bool sort)
        {
            Children.Add(new ScrollViewer { Content = new DishesList(category, ascendingOrder) });
            Children.Add(new ReturnButton());
            Children.Add(new SortButton(category));
        }

        class DishesList : StackPanel
        {
            public DishesList(Category category, bool sort)
            {
                Margin = new(20);
                Spacing = 10;
                HorizontalAlignment = HorizontalAlignment.Center;
                if (ascendingOrder)
                {
                    foreach (var dish in category.Dishes.OrderBy(d => d.PricePerPortion))
                        Children.Add(new DishBox(category, dish));
                }
                else
                {
                    foreach (var dish in category.Dishes.OrderByDescending(d => d.PricePerPortion))
                        Children.Add(new DishBox(category, dish));
                }
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
                                switch (count % 10)
                                {
                                    case 1 when count % 100 != 11:
                                        return "порция";
                                    case >= 2 and <= 4 when !(count % 100 >= 12 && count % 100 <= 14):
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
                    Width = 120,
                    Height = 50,
                    Margin = new(0),
                    Content = "🏠",
                    FontSize = 30,
                    Background = null,
                    Foreground = Brushes.DarkGray,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Command = ReactiveCommand.Create(
                        () => { _this.Content = new MainMenu(); })
                });
            }
        }

        class SortButton : Panel
        {
            private readonly Category _currentCategory;

            public SortButton(Category currentCategory)
            {
                _currentCategory = currentCategory;
                Children.Add(new Button
                {
                    Width = 100,
                    Height = 40,
                    Margin = new(20),
                    FontSize = 27,
                    Background = null,
                    Foreground = Brushes.DarkGray,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Command = ReactiveCommand.Create(ToggleSortOrder),
                    Content = CreateSortButtonContent()
                });
            }
            
            private object CreateSortButtonContent()
            {
                var sortSymbol = ascendingOrder ? "▲" : "▼";
                return new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = sortSymbol,
                            FontWeight = FontWeight.Bold,
                        }
                    }
                };
            }
            
            private void ToggleSortOrder()
            {
                ascendingOrder = !ascendingOrder;
                ((Button)Children[0]).Content = CreateSortButtonContent();

                _this.Content = new DishesMenu(_currentCategory, ascendingOrder);
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
                    Children.Add(new DishShortInfo(dish));
                    // Children.Add(new DishImage(dish));
                }

                class DishShortInfo : StackPanel
                {
                    public DishShortInfo(Dish dish)
                    {
                        Margin = new(20, 10);
                        Spacing = 10;
                        Children.Add(new DishName(dish.Name));
                        Children.Add(new RoundBorder(1)
                        {
                            Child = new TextBlock
                            {
                                Margin = new(10),
                                Text = new DishPrice(dish.Price).Text
                                       + "\n" + new DishPortionsCount(dish.NumberOfPortions).Text
                                       + "\n" + new DishPricePerPortion(dish.PricePerPortion).Text,
                                FontSize = 30,
                                FontWeight = FontWeight.SemiBold,
                                TextWrapping = TextWrapping.Wrap,
                                HorizontalAlignment = HorizontalAlignment.Left
                            }
                        });
                    }

                    private class DishName : Panel
                    {
                        public DishName(string name)
                        {
                            Children.Add(new RoundBorder(1)
                            {
                                Child = new TextBlock
                                {
                                    Margin = new(10),
                                    Text = name,
                                    FontSize = 30,
                                    FontWeight = FontWeight.SemiBold,
                                    TextWrapping = TextWrapping.Wrap,
                                    HorizontalAlignment = HorizontalAlignment.Left
                                }
                            });
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
                            Text = $"{DisplayPortionsNumber(count)} порций";
                        }

                        private string DisplayPortionsNumber(int count)
                        {
                            switch (count % 10)
                            {
                                case 1 when count % 100 != 11:
                                    return "порция";
                                case >= 2 and <= 4 when !(count % 100 >= 12 && count % 100 <= 14):
                                    return "порции";
                                default:
                                    return "порций";
                            }
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

            class Bottom : Grid
            {
                public Bottom(Dish dish)
                {
                    ColumnDefinitions = new ColumnDefinitions("*,*");

                    var ingredients = new Ingredients(dish);
                    var recipe = new Recipe(dish);

                    Children.Add(ingredients);
                    Children.Add(recipe);

                    SetColumn(ingredients, 0);
                    SetColumn(recipe, 1);
                    
                    ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
                    ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                }

                class Ingredients : StackPanel
                {
                    public Ingredients(Dish dish)
                    {
                        Margin = new(20, 0);
                        HorizontalAlignment = HorizontalAlignment.Stretch;
                        HorizontalAlignment = HorizontalAlignment.Left;
                        Children.Add(new RoundBorder(1)
                        {
                            Child = new IngredientsList(dish)
                        });
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
                            Children.Add(new Label());
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
                        HorizontalAlignment = HorizontalAlignment.Stretch;
                        HorizontalAlignment = HorizontalAlignment.Right;
                        Children.Add(new RoundBorder(1)
                        {
                            Child = new TextBlock
                            {
                                Margin = new(10),
                                Text = new Label().Text + "\n" + new Steps(dish).Text,
                                FontSize = 20,
                                FontWeight = FontWeight.SemiBold,
                                TextWrapping = TextWrapping.Wrap,
                            }
                        });
                    }

                    class Label : TextBlock
                    {
                        public Label()
                        {
                            Text = "Рецепт";
                            Margin = new(10);
                            FontSize = 26;
                            FontWeight = FontWeight.Bold;
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

        class RoundBorder : Border
        {
            public RoundBorder(int thickness)
            {
                BorderThickness = new Thickness(thickness);
                BorderBrush = Brushes.DarkSlateGray;
                CornerRadius = new CornerRadius(10); 
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
                        () => { _this.Content = new DishesMenu(category, false); })
                });
            }
        }
    }
}