using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace UserInterface.Views;

class ReturnToDishesMenuButton : Panel
{
    public ReturnToDishesMenuButton(MainWindow mainWindow, Category category, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 70,
            Height = 40,
            Content = new Image
            {
                Source = new Bitmap("Images/ReturnButton.png"),
                Width = 30,
                Height = 30,
            },
            Background = Brushes.Transparent,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new DishesMenu(mainWindow, category, categoryColor); })
        });
    }
}