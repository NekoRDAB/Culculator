using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class DishBox : Panel
{
    public DishBox(MainWindow mainWindow, Category category, Dish dish, bool ascendingOrder, Color categoryColor)
    {
        Width = 650;
        Height = 120;
        Children.Add(
            new DishDescriptionButton(mainWindow, category, dish, Width, Height, ascendingOrder, categoryColor));
        Children.Add(new DishName(dish.Name));
        Children.Add(new RightPart(dish));
    }
}