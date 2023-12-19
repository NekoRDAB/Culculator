using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Castle.Core.Smtp;

namespace UserInterface.Views;

class DishesMenu : Panel
{
    private static Color categoryColor;
    private static string sortType;
    private TextBox searchTextBox;
    private StackPanel contentStackPanel;

    private static readonly Dictionary<string, Color> CategoryColors = new()
    {
        { "Гарниры", Colors.AntiqueWhite },
        { "Завтраки", Colors.MintCream },
        { "Мясные блюда", Colors.LavenderBlush },
        { "Перекусы", Colors.Lavender }
    };

    public DishesMenu(MainWindow mainWindow, Category category, Color categoryColor)
    {
        categoryColor = CategoryColors[category.Name];
        Background = new SolidColorBrush(categoryColor);

        contentStackPanel = FormDishesMenu(mainWindow, category, categoryColor);
        Children.Add(new ScrollViewer { Content = contentStackPanel });
        
        searchTextBox = new TextBox
        {
            Watermark = "Рецепт",
            Width = 135,
            Height = 40,
            FontSize = 15,
            Margin = new Thickness(0,45,0,0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };
        searchTextBox.TextChanged += (sender, args) =>
        {
            Children.Remove(contentStackPanel);
            UpdateDisplayedDishes(mainWindow, category, categoryColor, searchTextBox.Text);
        };
        Children.Add(searchTextBox);
        
        var returnImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/ReturnButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        
        var addImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/AddButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        
        var returnToMainMenuButton = new BaseTargetButton(90, 50, returnImageContent, Brushes.Transparent, Brushes.Gray,null, VerticalAlignment.Bottom, HorizontalAlignment.Left,
            () => 
            { mainWindow.Content = new MainMenu(mainWindow, categoryColor); });
        Children.Add(returnToMainMenuButton);
        
        var goToAddRecipeWindowButton = new BaseTargetButton(90, 50, addImageContent,  Brushes.Transparent, Brushes.Gray,null,VerticalAlignment.Bottom, HorizontalAlignment.Right,
            () =>
        { mainWindow.Content = new AddRecipeLogic(mainWindow, category, categoryColor); });
        Children.Add(goToAddRecipeWindowButton);
        
        Children.Add(ContainerConfigurer.GetSortButton(mainWindow, category, categoryColor));
    }

    private StackPanel FormDishesMenu(MainWindow mainWindow, Category category, Color categoryColor)
    {
        var dishesStackPanel = new StackPanel()
        {
            Margin = new Thickness(20),
            Spacing = 10,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        foreach (var dish in category.Dishes)
            dishesStackPanel.Children.Add(new DishBox(mainWindow, category, dish,  categoryColor));
        return dishesStackPanel;
    }

    private void UpdateDisplayedDishes(MainWindow mainWindow, Category category, Color categoryColor, string searchText)
    {
        if (contentStackPanel != null)
        {
            contentStackPanel.Children.Clear(); 
            foreach (var dish in category.Dishes.Where(n => n.Name.ToLower().Contains(searchText.ToLower())))
            {
                contentStackPanel.Children.Add(new DishBox(mainWindow, category, dish, categoryColor));
            }
        }
        else
        {
            contentStackPanel = new StackPanel()
            {
                Margin = new Thickness(20),
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            foreach (var dish in category.Dishes.Where(n => n.Name.Contains(searchText)))
            {
                contentStackPanel.Children.Add(new DishBox(mainWindow, category, dish, categoryColor));
            }

            Children.Add(new ScrollViewer { Content = contentStackPanel });
        }
    }
}