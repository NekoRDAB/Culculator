using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

class CategoryButton : Panel
{
    public CategoryButton(MainWindow mainWindow, Category category, bool ascendingOrder, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 330,
            Height = 75,
            Content = category.Name,
            FontSize = 25,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new DishesMenu(mainWindow, category, ascendingOrder, categoryColor); })
        });
        Children.Add(new BlackBorder(330, 75, 1));
    }
}