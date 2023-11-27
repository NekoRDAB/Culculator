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
            Width = 200,
            Height = 40,
            Margin = new Thickness(20),
            Content = "В меню блюд",
            FontSize = 22,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new DishesMenu(mainWindow, category, ascendingOrder, categoryColor); })
        });
    }
}