using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishesList : StackPanel
{
    public DishesList(MainWindow mainWindow, Category category, bool ascendingOrder, Color categoryColor)
    {
        Margin = new Thickness(20);
        Spacing = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        if (ascendingOrder)
        {
            foreach (var dish in category.Dishes.OrderBy(d => d.PricePerPortion))
                Children.Add(new DishBox(mainWindow, category, dish, ascendingOrder, categoryColor));
        }
        else
        {
            foreach (var dish in category.Dishes.OrderByDescending(d => d.PricePerPortion))
                Children.Add(new DishBox(mainWindow, category, dish, ascendingOrder, categoryColor));
        }
    }
}