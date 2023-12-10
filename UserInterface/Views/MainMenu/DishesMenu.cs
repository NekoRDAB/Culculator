using System.Collections.Generic;
using Avalonia.Controls;
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
        Children.Add(new ReturnButton(mainWindow, categoryColor));
        Children.Add(new AddRecipeButton(mainWindow, category, categoryColor));
        Children.Add(ContainerConfigurer.GetSortButton(mainWindow, category, categoryColor));
    }
}