using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using UserInterface.Views;

namespace UserInterface.Views;

public class AddRecipeButton : Panel
{
    public AddRecipeButton(MainWindow mainWindow, Category category, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 120,
            Height = 50,
            Content = "Добавить",
            FontSize = 20,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Right,
            Command = ReactiveCommand.Create(
                () => 
                { 
                    mainWindow.Content = new AddRecipeWindow(mainWindow, category, categoryColor);
                    Console.WriteLine(category.Dishes.Count);
                })
        });
    }

    class AddRecipeWindow : Panel
    {
        public AddRecipeWindow(MainWindow mainWindow, Category category, Color categoryColor)
        {
            Children.Add(new AddRecipeParameters(mainWindow, category, categoryColor));
            Children.Add(new AddAndReturnButton(mainWindow, category, categoryColor));
        }
    }

    class AddRecipeParameters : Panel
    {
        public AddRecipeParameters(MainWindow mainWindow, Category category, Color categoryColor)
        {
            var stackPanel = new StackPanel();
            Children.Add(stackPanel);
        }
    }
    
    class AddAndReturnButton : Panel
    {
        public AddAndReturnButton(MainWindow mainWindow, Category category, Color categoryColor)
        {
            Children.Add(new Button
            {
                Width = 220,
                Height = 50,
                Content = "Добавить рецепт",
                FontSize = 20,
                Background = null,
                Foreground = Brushes.DarkGray,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Command = ReactiveCommand.Create(
                    () => 
                    { 
                        category.Dishes.Add(
                            new (new []{new Ingredient(1, "zssewdw", 0.2, "кг", 100)}, 
                                2, "recipe", "name" , category.Name
                            )); 
                        mainWindow.Content = new DishesMenu(mainWindow, category, false, categoryColor);
                        Console.WriteLine(category.Dishes.Count);
                    })
            });
        }
    }
}