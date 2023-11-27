using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UserInterface.Views;

class MainMenu : StackPanel
{
    public MainMenu(MainWindow mainWindow, bool ascendingOrder, Color categoryColor)
    {
        Margin = new Thickness(20);
        Spacing = 7;
        Children.Add(new Title());
        Children.Add(new CategoriesPanel(mainWindow, ascendingOrder, categoryColor));
    }
}