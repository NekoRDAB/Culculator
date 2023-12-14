using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

public class CategoriesPanel : StackPanel
{
    static ICategories _categories;

    public CategoriesPanel(MainWindow mainWindow, ICategories categories, Color categoryColor)
    {
        _categories = categories;
        Margin = new(20);
        Spacing = 10;
        foreach (var category in _categories.All)
        {
            var categoryContent = new ContentControl() { Content = category.Name };
            var categoryButton = new BaseTargetButton(330, 75, categoryContent, null, null, 25,
                VerticalAlignment.Center, HorizontalAlignment.Center,
                () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); });
            categoryButton.Children.Add(new BlackBorder(330, 75, 1));
            Children.Add(categoryButton);
        }
    }
}