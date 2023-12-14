using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

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
            { Content = new DishesList(mainWindow, category, categoryColor) });
        
        var returnToMainMenuButton = new BaseTargetButton(
            () => 
            { mainWindow.Content = new MainMenu(mainWindow, categoryColor); }, 
            "Images/ReturnButton.png", HorizontalAlignment.Left);
        Children.Add(returnToMainMenuButton);
        
        var goToAddRecipeWindowButton = new BaseTargetButton(
            () =>
        { mainWindow.Content = new AddRecipeLogic(mainWindow, category, categoryColor); },
            "Images/AddButton.png", HorizontalAlignment.Right);
        Children.Add(goToAddRecipeWindowButton);
        
        Children.Add(ContainerConfigurer.GetSortButton(mainWindow, category, categoryColor));
    }
}