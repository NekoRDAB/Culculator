using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

class DishDescriptionButton : Panel
{
    public DishDescriptionButton(MainWindow mainWindow, Category category, Dish dish, double width, double height,
        SortType sortType, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = width,
            Height = height,
            Command = ReactiveCommand.Create(
                () =>
                {
                    mainWindow.Content = new DishDescription(mainWindow, category, dish, sortType, categoryColor);
                })
        });
        Children.Add(new BlackBorder(width, height, 1));
    }
}