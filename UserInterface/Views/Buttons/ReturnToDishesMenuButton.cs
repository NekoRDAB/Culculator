using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

class ReturnToDishesMenuButton : Panel
{
    public ReturnToDishesMenuButton(MainWindow mainWindow, Category category, bool ascendingOrder, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 50,
            Height = 50,
            Margin = new Thickness(20),
            Content = "\ud83e\udc14",
            FontSize = 40,
            Background = null,
            Foreground = Brushes.Black,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new DishesMenu(mainWindow, category, ascendingOrder, categoryColor); })
        });
    }
}