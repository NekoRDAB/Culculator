using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishesList : StackPanel
{
    public DishesList(MainWindow mainWindow, Category category, Color categoryColor)
    {
        Margin = new Thickness(20);
        Spacing = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        foreach (var dish in category.Dishes)
            Children.Add(new DishBox(mainWindow, category, dish,  categoryColor));
    }
}