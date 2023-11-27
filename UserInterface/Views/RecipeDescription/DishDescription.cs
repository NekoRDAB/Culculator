using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class DishDescription : Panel
{
    public DishDescription(MainWindow mainWindow, Category category, Dish dish, bool ascendingOrder,
        Color categoryColor)
    {
        Children.Add(new DishDescriptionPanels(dish, categoryColor));
        Children.Add(new ReturnToDishesMenuButton(mainWindow, category, ascendingOrder, categoryColor));
    }
}