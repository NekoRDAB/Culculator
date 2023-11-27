using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using UserInterface.Views;

class ReturnButton : Panel
{
    public ReturnButton(MainWindow mainWindow, bool ascendingOrder, Color categoryColor)
    {
        Children.Add(new Button
        {
            Width = 120,
            Height = 50,
            Content = "🏠",
            FontSize = 30,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new MainMenu(mainWindow, ascendingOrder, categoryColor); })
        });
    }
}