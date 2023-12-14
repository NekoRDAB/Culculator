using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace UserInterface.Views;

class DishesMenu : Panel
{
    private static Color categoryColor;
    private static string sortType;

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
        
        Children.Add(new ScrollViewer
            { Content = FormDishesMenu(mainWindow, category, categoryColor) });
        
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
}