using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;
using UserInterface.Views;

class ReturnButton : Panel
{
    public ReturnButton(MainWindow mainWindow, Color categoryColor)
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
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(
                () => { mainWindow.Content = new MainMenu(mainWindow, categoryColor); })
        });
    }
}