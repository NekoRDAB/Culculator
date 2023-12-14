using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace UserInterface.Views;

class DishDescription : Panel
{
    public DishDescription(MainWindow mainWindow, Category category, Dish dish, Color categoryColor)
    {
        Children.Add(new DishDescriptionPanels(dish, categoryColor));
        var returnImageContent = new ContentControl()
        {
            Content = new Image
            {
                Source = new Bitmap("Images/ReturnButton.png"),
                Width = 30,
                Height = 30,
            }
        };
        var returnToDishesMenuButton = new BaseTargetButton(90, 50, returnImageContent, Brushes.Transparent, Brushes.Gray, null, VerticalAlignment.Bottom, HorizontalAlignment.Left,
            () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); });
        Children.Add(returnToDishesMenuButton);
    }
}