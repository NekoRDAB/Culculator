using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace UserInterface.Views;

class SortButton : Panel
{
    private readonly Category _currentCategory;
    private MainWindow mainWindow;
    private bool ascendingOrder;
    private Color categoryColor;

    public SortButton(MainWindow mainWindow, Category currentCategory, bool ascendingOrder, Color categoryColor)
    {
        this.mainWindow = mainWindow;
        _currentCategory = currentCategory;
        this.ascendingOrder = ascendingOrder;
        this.categoryColor = categoryColor;
        Children.Add(new Button
        {
            Width = 100,
            Height = 40,
            Margin = new Thickness(20),
            FontSize = 27,
            Background = null,
            Foreground = Brushes.DarkGray,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Command = ReactiveCommand.Create(ToggleSortOrder),
            Content = CreateSortButtonContent()
        });
    }

    private object CreateSortButtonContent()
    {
        var sortSymbol = ascendingOrder ? "▲" : "▼";
        return new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children =
            {
                new TextBlock
                {
                    Text = sortSymbol,
                    FontWeight = FontWeight.Bold,
                }
            }
        };
    }

    private void ToggleSortOrder()
    {
        ascendingOrder = !ascendingOrder;
        ((Button)Children[0]).Content = CreateSortButtonContent();

        mainWindow.Content = new DishesMenu(mainWindow, _currentCategory, ascendingOrder, categoryColor);
    }
}