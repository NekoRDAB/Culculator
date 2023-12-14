using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishBox : Panel
{
    public DishBox(MainWindow mainWindow, Category category, Dish dish, Color categoryColor)
    {
        var dishDescriptionButton = new BaseTargetButton(620, 120, null, null, null, null, VerticalAlignment.Center,
            HorizontalAlignment.Center, () =>
            {
                mainWindow.Content = new DishDescription(mainWindow, category, dish, categoryColor);
            });
        dishDescriptionButton.Children.Add(new BlackBorder(620, 120, 1));
        Children.Add(
            dishDescriptionButton);
        Children.Add(new DishName(dish.Name));
        Children.Add(new RightPart(dish));
    }
}