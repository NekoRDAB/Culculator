using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

public partial class MainWindow : Window
{
    private static bool ascendingOrder;
    private static double defaultMargin = 20;
    private static double textBlockMargin = 10;
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
            Margin = new Thickness(defaultMargin);
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
                Margin = new Thickness(textBlockMargin);
            }
        }

        class CategoriesPanel : StackPanel
        {
            public CategoriesPanel()
            {
                Margin = new(defaultMargin);
                Spacing = 10;
                var dir = Environment.CurrentDirectory;
                var pathToRecipes = dir.Replace("UserInterface", "Culculator\\RecipesDataBase.db");
                var pathToIngredients = dir.Replace("UserInterface", "Culculator\\IngredientsDataBase.db");
                var categories = new AutoCategories(pathToRecipes, pathToIngredients);
                foreach (var category in categories.All)
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
            Children.Add(new SortButton(category));
        }

        class DishesList : StackPanel
        {
            public DishesList(Category category)
            {
                Margin = new Thickness(defaultMargin);
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
                        Margin = new Thickness(textBlockMargin);
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
                            var priceTextBlock = new DishPrice(dish.Price);
                            Children.Add(priceTextBlock);
                            Children.Add(new InputPortionsCount(dish, priceTextBlock));
                            Children.Add(new DishPricePerPortion(dish.PricePerPortion));
                        }

                        class DishPrice : TextBlock
                        {
                            public DishPrice(double price)
                            {
                                Margin = new Thickness(textBlockMargin);
                                HorizontalAlignment = HorizontalAlignment.Right;
                                Text = $"{Math.Round(price, 2)} руб.";
                                FontSize = 22;
                            }
                        }

                        class DishPricePerPortion : TextBlock
                        {
                            public DishPricePerPortion(double price)
                            {
                                Margin = new Thickness(textBlockMargin);
                                HorizontalAlignment = HorizontalAlignment.Right;
                                VerticalAlignment = VerticalAlignment.Bottom;
                                Foreground = Brushes.Gray;
                                Text = $"{Math.Round(price, 2)} руб. за порцию";
                                FontSize = 22;
                            }
                        }
                    }

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
                                if (args.Key == Key.Enter)
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

                    class DishImage : Image
                    {
                        public DishImage(Dish dish)
                        {
                            Margin = new Thickness(textBlockMargin);
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
                    Margin = new Thickness(defaultMargin),
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

                _this.Content = new DishesMenu(_currentCategory);
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
                        Margin = new Thickness(20, 10);
                        Spacing = 10;
                        Children.Add(new DishName(dish.Name));
                        Children.Add(new RoundBorder(1)
                        {
                            Child = new TextBlock
                            {
                                Margin = new Thickness(textBlockMargin),
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
                                    Margin = new Thickness(textBlockMargin),
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
                            Margin = new Thickness(7, 3);
                            FontSize = 29;
                            Text = $"{(int)price} руб.";
                        }
                    }

                    private class DishPortionsCount : TextBlock
                    {
                        public DishPortionsCount(int count)
                        {
                            Margin = new Thickness(7, 3);
                            FontSize = 29;
                            Foreground = Brushes.Gray;
                            Text = $"{count} {DisplayPortionsNumber(count)}";
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
                            Margin = new Thickness(7, 3);
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
                        Margin = new Thickness(defaultMargin);
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
                        Margin = new Thickness(20, 0);
                        HorizontalAlignment = HorizontalAlignment.Stretch;
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
                            Margin = new Thickness(textBlockMargin);
                            FontSize = 30;
                            FontWeight = FontWeight.SemiBold;
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
                                    Margin = new Thickness(5);
                                    Text = $"• {ingredient.ToString()}";
                                    FontSize = 20;
                                    TextWrapping = TextWrapping.Wrap;
                                }
                            }
                        }
                    }
                }

                class Recipe : StackPanel
                {
                    public Recipe(Dish dish)
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch;
                        Margin = new Thickness(20, 0);

                        var labelTextBlock = new TextBlock
                        {
                            FontWeight = FontWeight.SemiBold,
                            Text = "Рецепт",
                            FontSize = 30,
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(textBlockMargin),
                        };

                        var recipeTextBlock = new TextBlock
                        {
                            Text = dish.FormatRecipe(),
                            FontSize = 20,
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(5),
                        };

                        Children.Add(new RoundBorder(1)
                        {
                            Child = new StackPanel
                            {
                                Children = { labelTextBlock, recipeTextBlock }
                            }
                        });
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
                    Margin = new Thickness(defaultMargin),
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