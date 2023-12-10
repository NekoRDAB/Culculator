using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishesList : StackPanel
{
    public DishesList(MainWindow mainWindow, Category category, SortType sortType, Color categoryColor)
    {
        Margin = new Thickness(20);
        Spacing = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        switch (sortType)
        {
            case SortType.AscendingByTotalPrice:
                foreach (var dish in category.Dishes.OrderBy(d => d.Price))
                    Children.Add(new DishBox(mainWindow, category, dish, sortType, categoryColor));
                break;
            case SortType.DescendingByTotalPrice:
                foreach (var dish in category.Dishes.OrderByDescending(d => d.Price))
                    Children.Add(new DishBox(mainWindow, category, dish, sortType, categoryColor));
                break;
            case SortType.AscendingByPortionPrice:
                foreach (var dish in category.Dishes.OrderBy(d => d.PricePerPortion))
                    Children.Add(new DishBox(mainWindow, category, dish, sortType, categoryColor));
                break;
            case SortType.DescendingByPortionPrice:
                foreach (var dish in category.Dishes.OrderByDescending(d => d.PricePerPortion))
                    Children.Add(new DishBox(mainWindow, category, dish, sortType, categoryColor));
                break;
        }
    }
}