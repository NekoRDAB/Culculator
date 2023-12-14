using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishDescription : Panel
{
    public DishDescription(MainWindow mainWindow, Category category, Dish dish, Color categoryColor)
    {
        Children.Add(new DishDescriptionPanels(dish, categoryColor));
        var returnToDishesMenuButton = new BaseTargetButton(
            () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); }, "Images/ReturnButton.png", HorizontalAlignment.Left);
        Children.Add(returnToDishesMenuButton);
    }
}