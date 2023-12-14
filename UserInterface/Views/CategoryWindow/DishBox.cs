using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace UserInterface.Views;

class DishBox : Panel
{
    public DishBox(MainWindow mainWindow, Category category, Dish dish, Color categoryColor)
    {
        var dishDescriptionButton = new BaseTargetButton(620, 120, null, null, null, null, VerticalAlignment.Center,
            HorizontalAlignment.Center, () =>
            {
                mainWindow.Content = new DishDescription(mainWindow, category, dish, categoryColor);
            });
        dishDescriptionButton.Children.Add(new BlackBorder(620, 120, 1));
        Children.Add(dishDescriptionButton);
        
        Children.Add(FormDishName(dish));
        var rightPart = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        rightPart.Children.Add(new DishShortDescription(dish));
        Children.Add(rightPart);
    }

    private TextBlock FormDishName(Dish dish)
    {
        var dishNameTextBlock = new TextBlock()
        {
            Margin = new Thickness(10),
            Text = dish.Name,
            FontSize = 30,
            MaxWidth = 420,
            FontWeight = FontWeight.SemiBold,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        return dishNameTextBlock;
    }
}